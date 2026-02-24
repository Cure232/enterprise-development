using Agency.Application.Contracts.Counterparties;
using Agency.Domain;
using Agency.Domain.Model;
using AutoMapper;

namespace Agency.Application.Services;

/// <summary>
/// Сервис для управления контрагентами
/// </summary>
public class CounterpartyService(
    IRepository<Counterparty, int> repository,
    IMapper mapper) : ICounterpartyService
{
    public async Task<CounterpartyDto> Create(CounterpartyCreateUpdateDto dto)
    {
        var entity = mapper.Map<Counterparty>(dto);
        var all = await repository.ReadAll();
        entity.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
        var result = await repository.Create(entity);
        return mapper.Map<CounterpartyDto>(result);
    }

    public async Task<bool> Delete(int dtoId) => await repository.Delete(dtoId);

    public async Task<CounterpartyDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId);
        return entity == null ? null : mapper.Map<CounterpartyDto>(entity);
    }

    public async Task<IList<CounterpartyDto>> GetAll()
    {
        var all = await repository.ReadAll();
        return mapper.Map<IList<CounterpartyDto>>(all);
    }

    public async Task<CounterpartyDto> Update(CounterpartyCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId) ?? throw new KeyNotFoundException($"Контрагент с Id {dtoId} не найден");
        mapper.Map(dto, entity);
        var result = await repository.Update(entity);
        return mapper.Map<CounterpartyDto>(result);
    }
}
