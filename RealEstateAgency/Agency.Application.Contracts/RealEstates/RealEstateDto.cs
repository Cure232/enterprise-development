using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// DTO объекта недвижимости
/// </summary>
/// <param name="Id"></param>
/// <param name="Type"></param>
/// <param name="Purpose"></param>
/// <param name="CadastralNumber"></param>
/// <param name="Address"></param>
/// <param name="TotalFloors"></param>
/// <param name="TotalArea"></param>
/// <param name="NumberOfRooms"></param>
/// <param name="CeilingHeight"></param>
/// <param name="Floor"></param>
/// <param name="HasEncumbrances"></param>
/// <param name="EncumbrancesDescription"></param>
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
