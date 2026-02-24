using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.ContractRequests;

/// <summary>
/// DTO для создания новой заявки или обновления существующей
/// Используется в HTTP POST (создание) и HTTP PUT (обновление) запросах
/// </summary>
/// <param name="CounterpartyId">Идентификатор контрагента (клиента), связанного с заявкой. Должен существовать в системе</param>
/// <param name="RealEstateId">Идентификатор объекта недвижимости, связанного с заявкой. Должен существовать в системе</param>
/// <param name="ContractRequestType">Тип заявки: Purchase (0) - покупка, Sale (1) - продажа</param>
/// <param name="Amount">Сумма сделки в рублях. Диапазон от 0 до 1 миллиарда рублей</param>
/// <param name="CreatedDate">Дата и время создания заявки в формате ISO 8601. Обычно устанавливается сервером</param>
/// <param name="Status">Текущий статус заявки. Максимальная длина 50 символов.</param>
public record ContractRequestCreateUpdateDto(
    int CounterpartyId,
    int RealEstateId,
    ContractRequestType ContractRequestType,
    [Range(0, 1_000_000_000)] decimal Amount,
    DateTime CreatedDate,
    [Required][StringLength(50)] string Status
);
