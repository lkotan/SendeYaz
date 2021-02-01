using System.ComponentModel.DataAnnotations;

namespace SendeYaz.Core.Enums
{
    public enum AccountType
    {
        [Display(Name ="Kullanıcı")] User=1,
        [Display(Name ="Yazar")] Author=10,
        [Display(Name ="Admin")] Admin=20,
    }
}
