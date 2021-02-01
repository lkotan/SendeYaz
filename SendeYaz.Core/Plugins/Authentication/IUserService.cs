using SendeYaz.Core.Plugins.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Plugins.Authentication
{
    public interface IUserService
    {
        UserInfo UserInfo { get; }
        int AccountId { get; }
        bool IsAdmin { get; }
        bool IsLogin { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
    }
}
