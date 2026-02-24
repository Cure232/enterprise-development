using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Назначение объекта недвижимости
/// </summary>
public enum RealEstatePurpose
{
    [Display(Name = "Жилое")]
    Residential,

    [Display(Name = "Коммерческое")]
    Commercial,

    [Display(Name = "Промышленное")]
    Industrial,

    [Display(Name = "Сельскохозяйственное")]
    Agricultural
}