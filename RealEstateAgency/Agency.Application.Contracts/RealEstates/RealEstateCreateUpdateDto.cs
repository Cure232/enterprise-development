using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// DTO для создания/обновления объекта недвижимости
/// </summary>
public record RealEstateCreateUpdateDto(
    RealEstateType Type,
    RealEstatePurpose Purpose,
    [Required][StringLength(50)] string CadastralNumber,
    [Required][StringLength(200)] string Address,
    int? TotalFloors,
    [Range(1, 10000)] double TotalArea,
    int? NumberOfRooms,
    double? CeilingHeight,
    int? Floor,
    bool HasEncumbrances,
    string? EncumbrancesDescription
);
