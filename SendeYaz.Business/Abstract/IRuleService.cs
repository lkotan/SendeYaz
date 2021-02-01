using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface IRuleService:IServiceRepository<RuleModel>
    {
        Task<IEnumerable<RuleListModel>> GetAllAsync(int roleId);

        Task<IEnumerable<IResponse>> SaveRangeAsync(IEnumerable<RuleModel> models);
    }
}
