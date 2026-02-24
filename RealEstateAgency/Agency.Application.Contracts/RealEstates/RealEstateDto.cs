using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// DTO для получения информации об объекте недвижимости
/// Используется в HTTP GET запросах для возврата данных клиенту
/// </summary>
/// <param name="Id">Уникальный идентификатор объекта недвижимости. Генерируется сервером при создании</param>
/// <param name="Type">Тип объекта недвижимости</param>
/// <param name="Purpose">Назначение объекта</param>
/// <param name="CadastralNumber">Кадастровый номер объекта</param>
/// <param name="Address">Полный почтовый адрес объекта</param>
/// <param name="TotalFloors">Общее количество этажей в здании</param>
/// <param name="TotalArea">Общая площадь объекта в квадратных метрах</param>
/// <param name="NumberOfRooms">Количество комнат</param>
/// <param name="CeilingHeight">Высота потолков в метрах</param>
/// <param name="Floor">Этаж расположения</param>
/// <param name="HasEncumbrances">Флаг наличия обременений</param>
/// <param name="EncumbrancesDescription">Описание обременений</param>
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
