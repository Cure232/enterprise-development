using Agency.Application.Contracts.Counterparties;
using Agency.Domain.Model;

namespace Agency.Application.Contracts;

/// <summary>
/// Сервис аналитики риэлторского агентства
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Продавцы, оставившие заявки на продажу за заданный период
    /// </summary>
    Task<IList<CounterpartyDto>> GetSellersWithRequestsInPeriod(DateTime from, DateTime to);

    /// <summary>
    /// Топ 5 покупателей по количеству заявок на покупку
    /// </summary>
    Task<IList<CounterpartyDto>> GetTop5BuyersByRequestCount();

    /// <summary>
    /// Топ 5 продавцов по количеству заявок на продажу
    /// </summary>
    Task<IList<CounterpartyDto>> GetTop5SellersByRequestCount();

    /// <summary>
    /// Количество заявок по каждому типу недвижимости
    /// </summary>
    Task<Dictionary<RealEstateType, int>> GetRequestCountByPropertyType();

    /// <summary>
    /// Клиенты с минимальной суммой заявки
    /// </summary>
    Task<IList<CounterpartyDto>> GetClientsWithMinimalRequestAmount();

    /// <summary>
    /// Клиенты, ищущие недвижимость заданного типа (покупка), упорядоченные по ФИО
    /// </summary>
    Task<IList<CounterpartyDto>> GetClientsSearchingForPropertyType(RealEstateType propertyType);

    /// <summary>
    /// Клиенты, ищущие дома (покупка), упорядоченные по ФИО
    /// </summary>
    Task<IList<CounterpartyDto>> GetClientsSearchingForHousesOrderedByFullName();
}
