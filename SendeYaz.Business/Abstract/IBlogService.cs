using SendeYaz.Core.Models;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface IBlogService:IServiceRepository<BlogModel>
    {
        Task<List<BlogListModel>> GetAllAsync(int? tagId);
        Task<BlogListModel> DetailAsync(int id);
        Task<List<BlogListModel>> GetAllByCategoryIdOrAccountIdAsync(int? categoryId,int? accountId);
        Task<IEnumerable<DropdownModel>> SelectListAsync();
    }
}
