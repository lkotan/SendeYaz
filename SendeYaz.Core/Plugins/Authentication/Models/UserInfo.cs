using SendeYaz.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Plugins.Authentication.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Rules = new List<AccountRule>();
        }

        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public List<AccountRule> Rules { get; set; }
    }
}
