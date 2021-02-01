using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Business.Validations;
using SendeYaz.Core.Aspect.Security;
using SendeYaz.Core.Aspect.Validation;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Plugins.Authentication;
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
    
    public class CommentService:ICommentService
    {
        private readonly IDataAccessRepository<Comment> _dal;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CommentService(IDataAccessRepository<Comment> dal,IUserService userService, IMapper mapper)
        {
            _dal = dal;

            _userService = userService;

            _mapper = mapper;
        }

        [SecurityAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            if (entity.Email != _userService.Email) return new ErrorDataResponse<int>(AccountMessage.AccountNotFound);
            return await _dal.DeleteAsync(entity);
        }

        [IsAdminAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }


        public async Task<CommentModel> GetAsync(int id)
        {
            return _mapper.Map<CommentModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }


        [SecurityAspect]
        [ValidationAspect(typeof(CommentValidator))]
        public async Task<IDataResponse<int>> InsertAsync(CommentModel model)
        {
            model.Name = $"{_userService.FirstName} {_userService.LastName}";
            model.Email = _userService.Email;
            return await _dal.InsertAsync(_mapper.Map<Comment>(model));
        }

        [SecurityAspect]
        [ValidationAspect(typeof(CommentValidator))]
        public async Task<IResponse> UpdateAsync(CommentModel model)
        {
            model.Name = $"{_userService.FirstName} {_userService.LastName}";
            model.Email = _userService.Email;
            return await _dal.UpdateAsync(_mapper.Map<Comment>(model));
        }


        public async Task<List<CommentListModel>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<CommentListModel> result = new List<CommentListModel>();
            await GetComments(blogId, parentId, result);
            return result;
        }


        private async Task GetComments(int blogId, int? parentId, List<CommentListModel> result)
        {
            var entities = _mapper.Map<List<CommentListModel>>(await _dal.TableNoTracking
                .Where(x => x.BlogId == blogId && x.ParentCommentId == parentId)
                .OrderByDescending(x => x.PostedTime)
                .ToListAsync());

            if (entities.Count > 0)
            {
                foreach (var comment in entities)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<CommentListModel>();

                    await GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
