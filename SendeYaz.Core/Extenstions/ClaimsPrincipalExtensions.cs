﻿using System.Linq;
using System.Security.Claims;

namespace SendeYaz.Core.Extenstions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.Name)?.Select(x => x.Value).FirstOrDefault();
        }

        public static string GetNameIdendifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).FirstOrDefault();
        }

        public static int GetAccountId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).FirstOrDefault()?.ToInt() ?? 0;
        }
    }
}
