using Agency.Application.Contracts.RealEstates;
using Agency.Domain;
using Agency.Domain.Model;
using AutoMapper;

namespace Agency.Application.Services;

/// <summary>
/// Сервис для управления объектами недвижимости
/// </summary>
public class RealEstateService(
    IRepository<RealEstate, int> repository,
    IMapper mapper) : IRealEstateService
{
    public async Task<RealEstateDto> Create(RealEstateCreateUpdateDto dto)
    {
        var entity = mapper.Map<RealEstate>(dto);
        var all = await repository.ReadAll();
        entity.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
        var result = await repository.Create(entity);
        return mapper.Map<RealEstateDto>(result);
    }

    public async Task<bool> Delete(int dtoId) => await repository.Delete(dtoId);

    public async Task<RealEstateDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId);
        return entity == null ? null : mapper.Map<RealEstateDto>(entity);
    }

    public async Task<IList<RealEstateDto>> GetAll()
    {
        var all = await repository.ReadAll();
        return mapper.Map<IList<RealEstateDto>>(all);
    }

    public async Task<RealEstateDto> Update(RealEstateCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId) ?? throw new KeyNotFoundException($"Объект недвижимости с Id {dtoId} не найден");
        mapper.Map(dto, entity);
        var result = await repository.Update(entity);
        return mapper.Map<RealEstateDto>(result);
    }
}
