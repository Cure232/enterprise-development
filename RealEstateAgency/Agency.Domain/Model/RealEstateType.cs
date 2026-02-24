using System.ComponentModel.DataAnnotations;

namespace Agency.Domain.Model;

/// <summary>
/// Тип объекта недвижимости
/// </summary>
public enum RealEstateType
{
    [Display(Name = "Квартира")]
    Apartment,

    [Display(Name = "Дом")]
    House,

    [Display(Name = "Земельный участок")]
    LandPlot,

    [Display(Name = "Коммерческая недвижимость")]
    Commercial,

    [Display(Name = "Гараж")]
    Garage
}