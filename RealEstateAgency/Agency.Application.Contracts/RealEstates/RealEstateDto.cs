using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// DTO объекта недвижимости
/// </summary>
public record RealEstateDto(
    int Id,
    RealEstateType Type,
    RealEstatePurpose Purpose,
    [Required][StringLength(50)] string CadastralNumber,
    [Required][StringLength(200)] string Address,
    int? TotalFloors,
    double TotalArea,
    int? NumberOfRooms,
    double? CeilingHeight,
    int? Floor,
    bool HasEncumbrances,
    string? EncumbrancesDescription
);
