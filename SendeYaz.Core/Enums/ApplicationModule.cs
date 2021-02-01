using System.ComponentModel.DataAnnotations;

namespace SendeYaz.Core.Enums
{
    public enum ApplicationModule
    {
        [Display(Name ="Null")] Null=0,
        [Display(Name ="Kullanıcılar")] Account=1,
        [Display(Name ="Kullanıcı Yetkileri")] Role=2,
        [Display(Name ="Bloglar")] Blog=3,
        [Display(Name ="Kategoriler")] Category=4,
        [Display(Name ="Etiketler")] Tag=5,
        [Display(Name ="Yorumlar")] Comment=6,
    }
}
