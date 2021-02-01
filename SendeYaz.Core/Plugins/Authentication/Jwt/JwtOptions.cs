using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Plugins.Authentication.Jwt
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
