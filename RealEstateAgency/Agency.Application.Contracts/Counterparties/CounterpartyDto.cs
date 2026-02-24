using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.Counterparties;

/// <summary>
/// DTO для получения информации о заявке
/// Используется в HTTP GET запросах для возврата данных клиенту
/// </summary>
/// <param name="Id">Уникальный идентификатор контрагента. Используется для ссылок на заявку, редактирования и удаления. Генерируется сервером при создании</param>
/// <param name="FullName">Полное имя контрагента в формате "Фамилия Имя Отчество". Максимальная длина 150 символов</param>
/// <param name="PassportNumber">Номер паспорта в формате "XXXX XXXXXX" (серия и номер). Максимальная длина 20 символов</param>
/// <param name="PhoneNumber">Контактный телефон в международном формате, например "+7 (XXX) XXX-XX-XX". Максимальная длина 20 символов</param>
public record CounterpartyDto(
    int Id,
    [Required][StringLength(150)] string FullName,
    [Required][StringLength(20)] string PassportNumber,
    [Required][StringLength(20)] string PhoneNumber
);
