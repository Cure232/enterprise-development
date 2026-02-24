using Agency.Application.Contracts;
using Agency.Application.Contracts.Counterparties;
using Agency.Domain;
using Agency.Domain.Model;
using AutoMapper;

namespace Agency.Application.Services;

/// <summary>
/// Сервис аналитики риэлторского агентства
/// </summary>
public class AnalyticsService(
    IRepository<ContractRequest, int> requestRepository,
    IMapper mapper) : IAnalyticsService
{
    public async Task<IList<CounterpartyDto>> GetSellersWithRequestsInPeriod(DateTime from, DateTime to)
    {
        var requests = await requestRepository.ReadAll();
        var sellers = requests
            .Where(r => r.ContractRequestType == ContractRequestType.Sale
                        && r.CreatedDate >= from
                        && r.CreatedDate <= to)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();
        return mapper.Map<IList<CounterpartyDto>>(sellers);
    }

    public async Task<IList<CounterpartyDto>> GetTop5BuyersByRequestCount()
    {
        var requests = await requestRepository.ReadAll();
        var buyersByCount = requests
            .Where(r => r.ContractRequestType == ContractRequestType.Purchase)
            .GroupBy(r => r.Counterparty)
            .Select(g => new { Counterparty = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Counterparty)
            .ToList();
        return mapper.Map<IList<CounterpartyDto>>(buyersByCount);
    }

    public async Task<IList<CounterpartyDto>> GetTop5SellersByRequestCount()
    {
        var requests = await requestRepository.ReadAll();
        var sellersByCount = requests
            .Where(r => r.ContractRequestType == ContractRequestType.Sale)
            .GroupBy(r => r.Counterparty)
            .Select(g => new { Counterparty = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => x.Counterparty)
            .ToList();
        return mapper.Map<IList<CounterpartyDto>>(sellersByCount);
    }

    public async Task<Dictionary<RealEstateType, int>> GetRequestCountByPropertyType()
    {
        var requests = await requestRepository.ReadAll();
        return requests
            .GroupBy(r => r.RealEstate.Type)
            .Select(g => new { PropertyType = g.Key, Count = g.Count() })
            .ToDictionary(x => x.PropertyType, x => x.Count);
    }

    public async Task<IList<CounterpartyDto>> GetClientsWithMinimalRequestAmount()
    {
        var requests = await requestRepository.ReadAll();
        var minAmount = requests.Min(r => r.Amount);
        var clients = requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();
        return mapper.Map<IList<CounterpartyDto>>(clients);
    }

    public async Task<IList<CounterpartyDto>> GetClientsSearchingForPropertyType(RealEstateType propertyType)
    {
        var requests = await requestRepository.ReadAll();
        var clients = requests
            .Where(r => r.ContractRequestType == ContractRequestType.Purchase && r.RealEstate.Type == propertyType)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();
        return mapper.Map<IList<CounterpartyDto>>(clients);
    }

    public async Task<IList<CounterpartyDto>> GetClientsSearchingForHousesOrderedByFullName()
    {
        return await GetClientsSearchingForPropertyType(RealEstateType.House);
    }
}
