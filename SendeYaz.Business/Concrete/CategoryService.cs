using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Business.Validations;
using SendeYaz.Core.Aspect.Caching;
using SendeYaz.Core.Aspect.Security;
using SendeYaz.Core.Aspect.Validation;
using SendeYaz.Core.Enums;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Concrete
{
    
    public class CategoryService : ICategoryService
    {
        private readonly IDataAccessRepository<Category> _dal;
        private readonly IMapper _mapper;
        public CategoryService(IDataAccessRepository<Category> dal,IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }
        [SecurityAspect]
        [RemoveCacheAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }


        [SecurityAspect]
        [RemoveCacheAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }


        [CacheAspect(20,1)]
        public async Task<List<CategoryModel>> GetAllAsync()
        {
            return _mapper.Map<List<CategoryModel>>(await _dal.TableNoTracking.ToListAsync());
        }


        public async Task<CategoryModel> GetAsync(int id)
        {
            return _mapper.Map<CategoryModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }


        [SecurityAspect]
        [RemoveCacheAspect]
        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IDataResponse<int>> InsertAsync(CategoryModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Category>(model));
        }



        [SecurityAspect]
        [RemoveCacheAspect("",1)]
        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResponse> UpdateAsync(CategoryModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Category>(model));
        }
    }
}
