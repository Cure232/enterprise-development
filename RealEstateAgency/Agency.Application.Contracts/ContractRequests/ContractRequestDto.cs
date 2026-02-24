using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.ContractRequests;

/// <summary>
/// DTO заявки
/// </summary>
public record ContractRequestDto(
    int Id,
    int CounterpartyId,
    int RealEstateId,
    ContractRequestType ContractRequestType,
    decimal Amount,
    DateTime CreatedDate,
    [Required][StringLength(50)] string Status
);
