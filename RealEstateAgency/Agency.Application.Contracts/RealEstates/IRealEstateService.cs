namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// Сервис для работы с объектами недвижимости
/// </summary>
public interface IRealEstateService : IApplicationService<RealEstateDto, RealEstateCreateUpdateDto, int>
{
}
