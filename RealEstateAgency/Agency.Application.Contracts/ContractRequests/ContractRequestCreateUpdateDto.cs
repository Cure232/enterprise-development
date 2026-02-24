using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.ContractRequests;

/// <summary>
/// DTO для создания/обновления заявки
/// </summary>
/// <param name="CounterpartyId"></param>
/// <param name="RealEstateId"></param>
/// <param name="ContractRequestType"></param>
/// <param name="Amount"></param>
/// <param name="CreatedDate"></param>
/// <param name="Status"></param>
public record ContractRequestCreateUpdateDto(
    int CounterpartyId,
    int RealEstateId,
    ContractRequestType ContractRequestType,
    [Range(0, 1_000_000_000)] decimal Amount,
    DateTime CreatedDate,
    [Required][StringLength(50)] string Status
);
