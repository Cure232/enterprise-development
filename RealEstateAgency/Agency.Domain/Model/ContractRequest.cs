using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Заявка (контракт)
/// </summary>
public class ContractRequest
{
    /// <summary>
    /// Уникальный идентификатор заявки
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Контрагент, связанный с заявкой
    /// </summary>
    public required Counterparty Counterparty { get; set; }

    /// <summary>
    /// Объект недвижимости, связанный с заявкой
    /// </summary>
    public required RealEstate RealEstate { get; set; }

    /// <summary>
    /// Тип заявки (покупка/продажа)
    /// </summary>
    public required ContractRequestType ContractRequestType { get; set; }

    /// <summary>
    /// Денежная сумма (в рублях)
    /// </summary>
    [Range(0, 1_000_000_000, ErrorMessage = "Сумма должна быть в диапазоне от 0 до 1 млрд рублей")]
    public required decimal Amount { get; set; }

    /// <summary>
    /// Дата создания заявки
    /// </summary>
    [DataType(DataType.Date)]
    public required DateTime CreatedDate { get; set; }

    /// <summary>
    /// Статус заявки
    /// </summary>
    [StringLength(50, ErrorMessage = "Статус не должен превышать 50 символов")]
    public required string Status { get; set; }
}