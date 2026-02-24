using Agency.Application.Contracts.ContractRequests;
using Agency.Domain;
using Agency.Domain.Model;
using AutoMapper;

namespace Agency.Application.Services;

/// <summary>
/// Сервис для управления заявками
/// </summary>
public class ContractRequestService(
    IRepository<ContractRequest, int> requestRepository,
    IRepository<Counterparty, int> counterpartyRepository,
    IRepository<RealEstate, int> realEstateRepository,
    IMapper mapper) : IContractRequestService
{
    public async Task<ContractRequestDto> Create(ContractRequestCreateUpdateDto dto)
    {
        var counterparty = await counterpartyRepository.Read(dto.CounterpartyId) ?? throw new KeyNotFoundException($"Контрагент с Id {dto.CounterpartyId} не найден");
        var realEstate = await realEstateRepository.Read(dto.RealEstateId) ?? throw new KeyNotFoundException($"Объект недвижимости с Id {dto.RealEstateId} не найден");

        var entity = mapper.Map<ContractRequest>(dto);
        entity.Counterparty = counterparty;
        entity.RealEstate = realEstate;
        var all = await requestRepository.ReadAll();
        entity.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
        var result = await requestRepository.Create(entity);
        return mapper.Map<ContractRequestDto>(result);
    }

    public async Task<bool> Delete(int dtoId) => await requestRepository.Delete(dtoId);

    public async Task<ContractRequestDto?> Get(int dtoId)
    {
        var entity = await requestRepository.Read(dtoId);
        return entity == null ? null : mapper.Map<ContractRequestDto>(entity);
    }

    public async Task<IList<ContractRequestDto>> GetAll()
    {
        var all = await requestRepository.ReadAll();
        return mapper.Map<IList<ContractRequestDto>>(all);
    }

    public async Task<ContractRequestDto> Update(ContractRequestCreateUpdateDto dto, int dtoId)
    {
        var entity = await requestRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Заявка с Id {dtoId} не найдена");
        var counterparty = await counterpartyRepository.Read(dto.CounterpartyId) ?? throw new KeyNotFoundException($"Контрагент с Id {dto.CounterpartyId} не найден");
        var realEstate = await realEstateRepository.Read(dto.RealEstateId) ?? throw new KeyNotFoundException($"Объект недвижимости с Id {dto.RealEstateId} не найден");

        mapper.Map(dto, entity);
        entity.Counterparty = counterparty;
        entity.RealEstate = realEstate;
        var result = await requestRepository.Update(entity);
        return mapper.Map<ContractRequestDto>(result);
    }
}
