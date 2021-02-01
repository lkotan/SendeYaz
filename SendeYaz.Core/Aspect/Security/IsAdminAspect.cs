using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using SendeYaz.Core.Plugins.Authentication;
using SendeYaz.Core.Utilities.Interceptors;
using SendeYaz.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using SendeYaz.Core.Exceptions;
using SendeYaz.Core.Messages;
using System.Linq;
using SendeYaz.Core.Extenstions;
using SendeYaz.Core.Enums;

namespace SendeYaz.Core.Aspect.Security
{
    public class IsAdminAspect:MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private LoggedInUsers _loggedInUsers;
        protected override void OnBefore(IInvocation invocation)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _loggedInUsers = ServiceTool.ServiceProvider.GetService<LoggedInUsers>();

            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var accountId = user.GetAccountId();
            if (accountId == 0)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var userInfo = _loggedInUsers.UserInfo.FirstOrDefault(x => x.AccountId == accountId);
            if (userInfo == null)
                throw new SecurityException(AspectMessage.AccessDenied);


            var isAdmin = userInfo.AccountType == AccountType.Admin;
            if (isAdmin)
                return;

            throw new SecurityException(AspectMessage.AccessDenied);

        }
    }
}
