using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.RealEstates;

/// <summary>
/// DTO для создания нового объекта недвижимости или обновления существующего
/// Используется в HTTP POST (создание) и HTTP PUT (обновление) запросах
/// </summary>
/// <param name="Type">Тип объекта недвижимости: Apartment (0) - квартира, House (1) - дом, LandPlot (2) - участок, Commercial (3) - коммерческая, Garage (4) - гараж</param>
/// <param name="Purpose">Назначение объекта: Residential (0) - жилое, Commercial (1) - коммерческое, Industrial (2) - промышленное, Agricultural (3) - сельскохозяйственное</param>
/// <param name="CadastralNumber">Кадастровый номер объекта в формате "XX:XX:XXXXXXX:XXXX". Максимальная длина 50 символов</param>
/// <param name="Address">Полный почтовый адрес объекта. Максимальная длина 200 символов</param>
/// <param name="TotalFloors">Общее количество этажей в здании. Указывается для домов и зданий, для участков может быть null</param>
/// <param name="TotalArea">Общая площадь объекта в квадратных метрах. Допустимый диапазон от 1 до 10000 кв.м</param>
/// <param name="NumberOfRooms">Количество комнат. Указывается для квартир и домов, для участков и коммерческой недвижимости может быть null</param>
/// <param name="CeilingHeight">Высота потолков в метрах. Указывается для помещений, для участков может быть null</param>
/// <param name="Floor">Этаж расположения. Указывается для квартир и помещений в многоэтажных зданиях</param>
/// <param name="HasEncumbrances">Флаг наличия обременений: true - есть обременения (ипотека, арест, аренда), false - нет обременений</param>
/// <param name="EncumbrancesDescription">Описание обременений, если HasEncumbrances = true. Необязательное поле, максимальная длина 500 символов</param>
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
