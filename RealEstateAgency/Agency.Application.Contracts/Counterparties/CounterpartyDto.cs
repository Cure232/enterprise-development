using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.Counterparties;

/// <summary>
/// DTO контрагента
/// </summary>
public record CounterpartyDto(
    int Id,
    [Required][StringLength(150)] string FullName,
    [Required][StringLength(20)] string PassportNumber,
    [Required][StringLength(20)] string PhoneNumber
);
