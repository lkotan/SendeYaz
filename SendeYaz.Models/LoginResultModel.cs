using SendeYaz.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class LoginResultModel
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public List<AccountRulesModel> Rules { get; set; }
    }
}
