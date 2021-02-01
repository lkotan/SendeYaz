using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface IRoleService:IServiceRepository<RoleModel>
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<IResponse> IsBlockedChangeAsync(int id);
    }
}
