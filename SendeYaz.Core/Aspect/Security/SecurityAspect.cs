﻿using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SendeYaz.Core.Enums;
using SendeYaz.Core.Exceptions;
using SendeYaz.Core.Extenstions;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Plugins.Authentication;
using SendeYaz.Core.Utilities.Interceptors;
using SendeYaz.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendeYaz.Core.Aspect.Security
{
    public class SecurityAspect:MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ApplicationModule _module;
        private RuleType _ruleType;
        private LoggedInUsers _loggedInUsers;

        public SecurityAspect()
        {

        }
        public SecurityAspect(ApplicationModule module)
        {
            _module = module;
        }
        public SecurityAspect(RuleType ruleType)
        {
            _ruleType = ruleType;
        }
        public SecurityAspect(ApplicationModule module, RuleType ruleType)
        {
            _module = module;
            _ruleType = ruleType;
        }


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

            var userInfo = _loggedInUsers.UserInfo.FirstOrDefault(x => x.AccountId ==accountId);
            if (userInfo == null)
                throw new SecurityException(AspectMessage.AccessDenied);

            var isAdmin = userInfo.AccountType == AccountType.Admin;
            if (isAdmin)
                return;

            if (userInfo.AccountType != AccountType.User && userInfo.AccountType!=AccountType.Author)
                throw new SecurityException(AspectMessage.AccessDenied);

            var actions = new[] { "GetAll", "Get", "Insert", "InsertRange", "Update", "Delete", "DeleteRange" };

            var action = invocation.Method.Name.Replace("Async", "");
            if (action.Left(8) == "GetAllBy") action = "GetAll";
            if(action.Right(6) == "Change" || action == "SaveRange") action = "Update";

            if (_ruleType == RuleType.Null && !actions.Contains(action)) return;

            if (_module == ApplicationModule.Null)
            {
                var serviceName = invocation.TargetType?.Name ?? "";
                serviceName = serviceName.Left(serviceName.Length - "Service".Length);

                try
                {
                    _module = serviceName.ToEnum<ApplicationModule>();
                }
                catch (Exception e)
                {
                    throw new Exception($"{AspectMessage.ApplicationModuleNotDefined} {e.Message}");
                }
            }

            if (_ruleType == RuleType.Null)
            {
                if (action.Contains("Get"))
                {
                    _ruleType = RuleType.View;
                }
                else if (action.Contains("Insert") || action.Contains("InsertRange"))
                {
                    _ruleType = RuleType.Insert;
                }
                else if (action.Contains("Update"))
                {
                    _ruleType = RuleType.Update;
                }
                else if (action.Contains("Delete") || action.Contains("DeleteRange"))
                {
                    _ruleType = RuleType.Delete;
                }
            }


            var rules = userInfo.Rules.FirstOrDefault(x => x.ApplicationModule == _module);

            var isAuthorized = rules != null && _ruleType switch
            {
                RuleType.View => rules.View,
                RuleType.Insert => rules.Insert,
                RuleType.Update => rules.Update,
                RuleType.Delete => rules.Delete,
                _ => false
            };
            if (isAuthorized) return;
            throw new SecurityException(AspectMessage.AccessDenied);
        }
    }
}
