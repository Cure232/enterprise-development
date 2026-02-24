using Agency.Domain;
using Agency.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий объектов недвижимости
/// </summary>
public class RealEstateRepository(AgencyDbContext context) : IRepository<RealEstate, int>
{
    public async Task<RealEstate> Create(RealEstate entity)
    {
        var entry = await context.RealEstates.AddAsync(entity);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.RealEstates.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null) return false;
        context.RealEstates.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<RealEstate?> Read(int entityId) =>
        await context.RealEstates.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<RealEstate>> ReadAll() =>
        await context.RealEstates.ToListAsync();

    public async Task<RealEstate> Update(RealEstate entity)
    {
        context.RealEstates.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
