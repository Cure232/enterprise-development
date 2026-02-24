using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Тип заявки
/// </summary>
public enum ContractRequestType
{
    [Display(Name = "Покупка")]
    Purchase,

    [Display(Name = "Продажа")]
    Sale
}