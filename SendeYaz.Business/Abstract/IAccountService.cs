using SendeYaz.Core.Models;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface IAccountService:IServiceRepository<AccountModel>
    {
        Task<IEnumerable<AccountModel>> GetAllAsync();
        Task<IEnumerable<DropdownModel>> SelectListAsync();
        Task<IResponse> IsBlockedChangeAsync(int id);
        Task<IResponse> UpdateMeAsync(AccountMeModel model);
    }
}
