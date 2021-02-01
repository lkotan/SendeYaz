using Microsoft.AspNetCore.Http;
using SendeYaz.Core.Enums;
using SendeYaz.Core.Extenstions;
using SendeYaz.Core.Plugins.Authentication.Models;
using System.Linq;

namespace SendeYaz.Core.Plugins.Authentication
{
    public class UserJwtService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggedInUsers _loggedInUsers;

        public UserJwtService(IHttpContextAccessor httpContextAccessor, LoggedInUsers loggedInUsers)
        {
            _httpContextAccessor = httpContextAccessor;
            _loggedInUsers = loggedInUsers;
        }

        private UserInfo GetUser()
        {
            var userInfo = _httpContextAccessor?.HttpContext?.User;
            var accountId = userInfo.GetAccountId();

            return _loggedInUsers.UserInfo.FirstOrDefault(x => x.AccountId == accountId);
        }

        public UserInfo UserInfo => GetUser();

        public int AccountId => GetUser().AccountId;

        public bool IsAdmin => AccountType == AccountType.Admin;

        public bool IsLogin => GetUser() != null;

        public string FirstName => GetUser().FirstName;

        public string LastName => GetUser().LastName;

        public string Email => GetUser().Email;

        private AccountType AccountType => GetUser().AccountType;
    }
}
