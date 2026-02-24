using Agency.Application.Contracts.ContractRequests;
using Agency.Application.Contracts.Counterparties;
using Agency.Application.Contracts.Protos;
using Agency.Application.Contracts.RealEstates;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Agency.Api.Host.Grpc;

/// <summary>
/// Фоновый gRPC клиент для получения батчей ContractRequestCreateUpdateDto из bidirectional стрима и создания заявок в системе
/// </summary>
public class AgencyGrpcClient(
    ContractRequestGeneratorGrpcService.ContractRequestGeneratorGrpcServiceClient client,
    IServiceScopeFactory scopeFactory,
    IMapper mapper,
    ILogger<AgencyGrpcClient> logger,
    IConfiguration cfg,
    IMemoryCache cache
) : BackgroundService
{
    private static readonly TimeSpan _cacheTtl = TimeSpan.FromMinutes(10);

    /// <summary>
    /// Основной цикл фонового сервиса который подключается к gRPC серверу отправляет запрос генерации и обрабатывает входящие батчи
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var countPerRequest = cfg.GetValue("ContractRequestGenerator:CountPerRequest", 100);
                var batchSize = cfg.GetValue("ContractRequestGenerator:BatchSize", 10);

                logger.LogInformation("Connecting to ContractRequestGenerator gRPC bidirectional stream...");

                using var call = client.ContractRequestStream(cancellationToken: stoppingToken);

                var requestId = Guid.NewGuid().ToString("N");

                var writerTask = Task.Run(async () =>
                {
                    await call.RequestStream.WriteAsync(new ContractRequestGenerationRequest
                    {
                        RequestId = requestId,
                        Count = countPerRequest,
                        BatchSize = batchSize
                    });

                    await call.RequestStream.CompleteAsync();
                }, stoppingToken);

                await foreach (var msg in call.ResponseStream.ReadAllAsync(stoppingToken))
                {
                    if (!string.Equals(msg.RequestId, requestId, StringComparison.Ordinal))
                        continue;

                    var dtos = msg.ContractRequests.Select(mapper.Map<ContractRequestCreateUpdateDto>).ToList();

                    using var scope = scopeFactory.CreateScope();

                    var contractRequestService = scope.ServiceProvider.GetRequiredService<IContractRequestService>();
                    var counterpartyService = scope.ServiceProvider.GetRequiredService<ICounterpartyService>();
                    var realEstateService = scope.ServiceProvider.GetRequiredService<IRealEstateService>();

                    var valid = new List<ContractRequestCreateUpdateDto>(dtos.Count);

                    foreach (var dto in dtos)
                    {
                        if (!await ExistsAsync(dto.CounterpartyId, "Counterparty", dto, counterpartyService.Get, stoppingToken))
                            continue;

                        if (!await ExistsAsync(dto.RealEstateId, "RealEstate", dto, realEstateService.Get, stoppingToken))
                            continue;

                        valid.Add(dto);
                    }

                    var created = 0;
                    foreach (var dto in valid)
                    {
                        await contractRequestService.Create(dto);
                        created++;
                    }

                    logger.LogInformation("Received batch: total={total}, valid={valid}, created={created}, isFinal={isFinal}", dtos.Count, valid.Count, created, msg.IsFinal);

                    if (msg.IsFinal)
                        break;
                }

                await writerTask;

                logger.LogInformation("Finished receiving contract requests for request_id={requestId}", requestId);
                break;
            }
            catch (RpcException ex) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogError(ex, "gRPC stream error: {code} - {status}", ex.StatusCode, ex.Status);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogError(ex, "Unexpected exception during receiving contract requests from gRPC stream");
                break;
            }
        }
    }

    /// <summary>
    /// Проверка наличия сущности по идентификатору с использованием IMemoryCache чтобы не выполнять повторные запросы
    /// </summary>
    private async Task<bool> ExistsAsync<TEntity>(
        int id,
        string entityName,
        ContractRequestCreateUpdateDto dto,
        Func<int, Task<TEntity?>> readFunc,
        CancellationToken ct)
        where TEntity : class
    {
        var cacheKey = $"{entityName}:exists:{id}";

        if (cache.TryGetValue(cacheKey, out bool cached))
            return cached;

        ct.ThrowIfCancellationRequested();

        bool exists;
        try
        {
            var entity = await readFunc(id);
            exists = entity is not null;
        }
        catch (KeyNotFoundException)
        {
            exists = false;

            logger.LogWarning(
                "Skipping contract request dto because {entity} with id {id} was not found counterpartyId={counterpartyId} realEstateId={realEstateId}",
                entityName, id, dto.CounterpartyId, dto.RealEstateId);
        }

        cache.Set(cacheKey, exists, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheTtl
        });

        return exists;
    }
}
