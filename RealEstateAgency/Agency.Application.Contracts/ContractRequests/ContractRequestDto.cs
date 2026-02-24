using Agency.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.ContractRequests;

/// <summary>
/// DTO заявки
/// </summary>
/// <param name="Id"></param>
/// <param name="CounterpartyId"></param>
/// <param name="RealEstateId"></param>
/// <param name="ContractRequestType"></param>
/// <param name="Amount"></param>
/// <param name="CreatedDate"></param>
/// <param name="Status"></param>
public record ContractRequestDto(
    int Id,
    int CounterpartyId,
    int RealEstateId,
    ContractRequestType ContractRequestType,
    decimal Amount,
    DateTime CreatedDate,
    [Required][StringLength(50)] string Status
);
