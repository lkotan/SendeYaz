using SendeYaz.Core.Enums;
using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;

namespace SendeYaz.Entities
{
    public class Account:IBaseEntity
    {
        public int Id { get; set; }
        public AccountType AccountType { get; set; } 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public string ProfilePhoto { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredDate { get; set; }
        public int? RoleId { get; set; }
        public bool IsBlocked { get; set; }

        public Role Role { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
