using SendeYaz.Core.Enums;
using SendeYaz.Core.Signatures;

namespace SendeYaz.Models
{
    public class AccountModel:IBaseModel
    {
        public int Id { get; set; }
        public AccountType AccountType { get; set; } = AccountType.User;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public string ProfilePhoto { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
