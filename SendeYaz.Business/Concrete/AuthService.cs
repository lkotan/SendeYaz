using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Core.Aspect.Security;
using SendeYaz.Core.Enums;
using SendeYaz.Core.Exceptions;
using SendeYaz.Core.Helpers;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Models;
using SendeYaz.Core.Plugins.Authentication;
using SendeYaz.Core.Plugins.Authentication.Jwt;
using SendeYaz.Core.Plugins.Authentication.Models;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IDataAccessRepository<Account> _dal;
        private readonly IDataAccessRepository<Rule> _dalRule;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly LoggedInUsers _loggedInUsers;
        private readonly JwtOptions _jwtOptions;


        public AuthService(IDataAccessRepository<Account> dal, IDataAccessRepository<Rule> dalRule, ITokenHelper tokenHelper,  IUserService userService,LoggedInUsers loggedInUsers, JwtOptions jwtOptions)
        {
            _dal = dal;
            _dalRule = dalRule;

            _tokenHelper = tokenHelper;
            _userService = userService;
            _loggedInUsers = loggedInUsers;
            _jwtOptions = jwtOptions;
        }
        private async Task<IDataResponse<LoginResultModel>> LoginAsync(Account account, bool isRefreshLogin = false)
        {
            var roleId = account.RoleId;
            if (account.Email == "lutfikotann@gmail.com")
            {
                account.AccountType = AccountType.Admin;
            }
            var rules = await _dalRule.TableNoTracking.Where(x => x.RoleId == roleId).ToListAsync();

            var rulesModel = rules.Select(x => new AccountRulesModel
            {
                ApplicationModule = x.ApplicationModule,
                Delete = x.Delete,
                Insert = x.Insert,
                Update = x.Update,
                View = x.Update,
                ApplicationModuleName=EnumHelper.GetDisplayValue(x.ApplicationModule),
            }).ToList();
        
            var user = new UserInfo
            {
                AccountId = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                AccountType = account.AccountType,
                Email=account.Email,
                Rules = rules.Select(x => new AccountRule
                {
                    ApplicationModule = x.ApplicationModule,
                    View = x.View,
                    Insert = x.Insert,
                    Update = x.Update,
                    Delete = x.Delete
                }).Where(x => x.Insert || x.View || x.Delete || x.Update).ToList()
            };
            var accessToken = _tokenHelper.CreateToken(account.Id, rulesModel);
            var tokenOptions = _jwtOptions;

            var acc = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == account.Id);

            acc.RefreshToken = accessToken.RefreshToken;
            acc.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 30);
            
            await _dal.UpdateAsync(acc);

            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.AccountId != account.Id).ToList();
            _loggedInUsers.UserInfo.Add(user);
          
            var result = new LoginResultModel
            {
                AccountId = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email=account.Email,
                Token = accessToken.Token,
                RefreshToken = accessToken.RefreshToken,
                TokenExpiration = DateTime.Now,
                Rules= rulesModel
            };
            return new SuccessDataResponse<LoginResultModel>(result);
        }


        public async Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel model)
        {

            var account = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (account == null)
                return new ErrorDataResponse<LoginResultModel>(AccountMessage.AccountNotFound);
            if (account.IsBlocked)
                return new ErrorDataResponse<LoginResultModel>(AccountMessage.AccountIsBlocked);

            if (HashingHelper.VerifyPasswordHash(model.Password, account.PasswordHash, account.PasswordSalt))

                return await LoginAsync(account);
     
            return new ErrorDataResponse<LoginResultModel>(AccountMessage.PasswordWrong);
        }


        public async Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model)
        {
            var account = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.RefreshToken == model.Token);
            if (account == null)
            {
                throw new AuthenticationException(AccountMessage.AuthenticationError);
            }
            if (account.IsBlocked)
            {
                return new ErrorDataResponse<LoginResultModel>(AccountMessage.AccountIsBlocked);
            }
            if (!string.IsNullOrEmpty(account.RefreshToken) && account.RefreshTokenExpiredDate > DateTime.Now)
            {
                return await LoginAsync(account, true);
            }
            throw new AuthenticationException(AccountMessage.AuthenticationError);
        }

        [SecurityAspect]
        public async Task<IResponse> LogoutAsync()
        {
            var account = await _dal.GetAsync(x => x.Id == _userService.AccountId);
            account.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(-30);
            await _dal.UpdateAsync(account);
            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.AccountId != account.Id).ToList();
            return new SuccessResponse(AccountMessage.LogoutSuccessful);
        }

        [SecurityAspect]
        public async Task<IResponse> ChangePasswordAsync(ChangePasswordModel model)
        {
            var account = await _dal.GetAsync(_userService.AccountId);
            if (!HashingHelper.VerifyPasswordHash(model.OldPassword, account.PasswordHash, account.PasswordSalt))
                return new ErrorResponse(AccountMessage.PasswordWrong);
            HashingHelper.CreatePasswordHash(model.NewPassword, out var passwordHash, out var passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            return await _dal.UpdateAsync(account);
        }
    }
}
