using Agency.Domain;
using Agency.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий заявок (MongoDB — навигационные свойства заполняются вручную)
/// </summary>
public class ContractRequestRepository(AgencyDbContext context) : IRepository<ContractRequest, int>
{
    public async Task<ContractRequest> Create(ContractRequest entity)
    {
        var entry = await context.ContractRequests.AddAsync(entity);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.ContractRequests.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null) return false;
        context.ContractRequests.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<ContractRequest?> Read(int entityId)
    {
        var entity = await context.ContractRequests.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null) return null;
        await FillNavigationProperties(new[] { entity });
        return entity;
    }

    public async Task<IList<ContractRequest>> ReadAll()
    {
        var list = await context.ContractRequests.ToListAsync();
        await FillNavigationProperties(list);
        return list;
    }

    public async Task<ContractRequest> Update(ContractRequest entity)
    {
        context.ContractRequests.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    private async Task FillNavigationProperties(IList<ContractRequest> requests)
    {
        if (requests.Count == 0) return;
        var counterpartyIds = requests.Select(r => r.CounterpartyId).Distinct().ToList();
        var realEstateIds = requests.Select(r => r.RealEstateId).Distinct().ToList();
        var counterparties = await context.Counterparties.Where(c => counterpartyIds.Contains(c.Id)).ToListAsync();
        var realEstates = await context.RealEstates.Where(r => realEstateIds.Contains(r.Id)).ToListAsync();
        var cpMap = counterparties.ToDictionary(c => c.Id);
        var reMap = realEstates.ToDictionary(r => r.Id);
        foreach (var r in requests)
        {
            if (cpMap.TryGetValue(r.CounterpartyId, out var cp)) r.Counterparty = cp;
            if (reMap.TryGetValue(r.RealEstateId, out var re)) r.RealEstate = re;
        }
    }
}
