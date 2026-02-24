using System.ComponentModel.DataAnnotations;

namespace Agency.Application.Contracts.Counterparties;

/// <summary>
/// DTO для создания/обновления контрагента
/// </summary>
/// <param name="FullName"></param>
/// <param name="PassportNumber"></param>
/// <param name="PhoneNumber"></param>
public record CounterpartyCreateUpdateDto(
    [Required][StringLength(150)] string FullName,
    [Required][StringLength(20)] string PassportNumber,
    [Required][StringLength(20)][Phone] string PhoneNumber
);
