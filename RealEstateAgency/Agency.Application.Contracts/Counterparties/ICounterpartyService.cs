namespace Agency.Application.Contracts.Counterparties;

/// <summary>
/// Сервис для работы с контрагентами
/// </summary>
public interface ICounterpartyService : IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int>
{
}
