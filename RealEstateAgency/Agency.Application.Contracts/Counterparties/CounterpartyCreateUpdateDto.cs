using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.Counterparties;

/// <summary>
/// DTO для создания/обновления контрагента
/// </summary>
public record CounterpartyCreateUpdateDto(
    [Required][StringLength(150)] string FullName,
    [Required][StringLength(20)] string PassportNumber,
    [Required][StringLength(20)][Phone] string PhoneNumber
);
