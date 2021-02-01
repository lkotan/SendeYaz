using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel model);
        Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model);
        Task<IResponse> LogoutAsync();
        Task<IResponse> ChangePasswordAsync(ChangePasswordModel model);
    }
}
