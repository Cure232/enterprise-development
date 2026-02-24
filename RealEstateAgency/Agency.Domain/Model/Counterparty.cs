using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Контрагент (клиент агентства)
/// </summary>
public class Counterparty
{
    /// <summary>
    /// Уникальный идентификатор контрагента
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// ФИО контрагента
    /// </summary>
    [StringLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [StringLength(20, ErrorMessage = "Номер паспорта не должен превышать 20 символов")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    [StringLength(20, ErrorMessage = "Номер телефона не должен превышать 20 символов")]
    [Phone(ErrorMessage = "Неверный формат номера телефона")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Список заявок/контрактов, связанных с данным контрагентом
    /// </summary>
    public List<ContractRequest>? Requests { get; set; } = [];
}