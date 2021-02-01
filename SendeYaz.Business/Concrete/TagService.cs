using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Business.Validations;
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
   
    public class TagService : ITagService
    {
        private readonly IDataAccessRepository<Tag> _dal;
        private readonly IMapper _mapper;

        public TagService(IDataAccessRepository<Tag> dal,IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }
        [SecurityAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }
        [SecurityAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }

        public async Task<IEnumerable<TagModel>> GetAllAsync()
        {
            return _mapper.Map<List<TagModel>>(await _dal.TableNoTracking.ToListAsync());
        }

        public async Task<TagModel> GetAsync(int id)
        {
            return _mapper.Map<TagModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [SecurityAspect]
        [ValidationAspect(typeof(TagValidator))]
        public async Task<IDataResponse<int>> InsertAsync(TagModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Tag>(model));
        }

        [SecurityAspect]
        [ValidationAspect(typeof(TagValidator))]
        public async Task<IResponse> UpdateAsync(TagModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Tag>(model));
        }
    }
}
