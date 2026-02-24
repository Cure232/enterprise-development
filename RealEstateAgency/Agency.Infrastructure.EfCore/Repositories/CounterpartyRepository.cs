using Agency.Domain;
using Agency.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий контрагентов
/// </summary>
public class CounterpartyRepository(AgencyDbContext context) : IRepository<Counterparty, int>
{
    public async Task<Counterparty> Create(Counterparty entity)
    {
        var entry = await context.Counterparties.AddAsync(entity);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Counterparties.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null) return false;
        context.Counterparties.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Counterparty?> Read(int entityId) =>
        await context.Counterparties.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<Counterparty>> ReadAll() =>
        await context.Counterparties.ToListAsync();

    public async Task<Counterparty> Update(Counterparty entity)
    {
        context.Counterparties.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
