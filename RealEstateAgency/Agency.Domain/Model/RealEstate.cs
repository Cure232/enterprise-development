using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Объект недвижимости
/// </summary>
public class RealEstate
{
    /// <summary>
    /// Уникальный идентификатор объекта недвижимости
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Тип объекта недвижимости
    /// </summary>
    public required RealEstateType Type { get; set; }

    /// <summary>
    /// Назначение объекта недвижимости
    /// </summary>
    public required RealEstatePurpose Purpose { get; set; }

    /// <summary>
    /// Кадастровый номер
    /// </summary>
    [StringLength(50, ErrorMessage = "Кадастровый номер не должен превышать 50 символов")]
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Адрес объекта
    /// </summary>
    [StringLength(200, ErrorMessage = "Адрес не должен превышать 200 символов")]
    public required string Address { get; set; }

    /// <summary>
    /// Этажность здания
    /// </summary>
    [Range(1, 200, ErrorMessage = "Этажность должна быть в диапазоне от 1 до 200")]
    public int? TotalFloors { get; set; }

    /// <summary>
    /// Общая площадь (в кв. метрах)
    /// </summary>
    [Range(1, 10000, ErrorMessage = "Общая площадь должна быть в диапазоне от 1 до 10 000 кв. м")]
    public required double TotalArea { get; set; }

    /// <summary>
    /// Количество комнат
    /// </summary>
    [Range(1, 50, ErrorMessage = "Количество комнат должно быть в диапазоне от 1 до 50")]
    public int? NumberOfRooms { get; set; }

    /// <summary>
    /// Высота потолков (в метрах)
    /// </summary>
    [Range(1.5, 10, ErrorMessage = "Высота потолков должна быть в диапазоне от 1.5 до 10 метров")]
    public double? CeilingHeight { get; set; }

    /// <summary>
    /// Этаж расположения
    /// </summary>
    [Range(1, 200, ErrorMessage = "Этаж расположения должен быть в диапазоне от 1 до 200")]
    public int? Floor { get; set; }

    /// <summary>
    /// Наличие обременений
    /// </summary>
    public required bool HasEncumbrances { get; set; }

    /// <summary>
    /// Описание обременений
    /// </summary>
    [StringLength(500, ErrorMessage = "Описание обременений не должно превышать 500 символов")]
    public string? EncumbrancesDescription { get; set; }

    /// <summary>
    /// Список заявок/контрактов, связанных с данным объектом недвижимости
    /// </summary>
    public List<ContractRequest>? Requests { get; set; } = [];
}