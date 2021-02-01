using SendeYaz.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Plugins.Authentication.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(int accountId,List<AccountRulesModel> rules);
    }
}
