using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Business.Validations;
using SendeYaz.Core.Aspect.Caching;
using SendeYaz.Core.Aspect.Security;
using SendeYaz.Core.Aspect.Validation;
using SendeYaz.Core.Exceptions;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Models;
using SendeYaz.Core.Plugins.Authentication;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendeYaz.Business.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IDataAccessRepository<Blog> _dal;
        private readonly IDataAccessRepository<BlogTag> _dalBlogTag;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BlogService(IDataAccessRepository<Blog> dal, IDataAccessRepository<BlogTag> dalBlogTag, IUserService userService, IMapper mapper)
        {
            _dal = dal;
            _dalBlogTag = dalBlogTag;

            _mapper = mapper;
            _userService = userService;
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
        

        public async Task<BlogListModel> DetailAsync(int id)
        {
            return await _dal.TableNoTracking
                .Select(x => new BlogListModel
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    Title = x.Title,
                    AccountInfo = new AccountInfoModel
                    {
                        Id = x.AccountId,
                        FirstName = x.Account.FirstName,
                        LastName = x.Account.LastName,
                        ProfilePhoto = x.Account.ProfilePhoto
                    },
                    TagNames = x.BlogTags.Where(x => x.BlogId == id).Select(x => new BlogTagModel
                    {
                        TagName = x.Tag.Name,
                        TagId = x.Tag.Id
                    }).ToList(),
                }).FirstOrDefaultAsync(x => x.Id == id);
        }


        [CacheAspect(20,0)]
        public async Task<List<BlogListModel>> GetAllAsync(int? tagId)
        {
            return _mapper.Map<List<BlogListModel>>(await _dal.TableNoTracking
                 .Where(x => !x.Account.IsBlocked)
                 .Where(x => tagId != null ? x.BlogTags.Any(x => x.TagId == tagId) : x.Id > 0)
                 .Select(x => new BlogListModel
                 {
                     Id = x.Id,
                     CategoryId = x.CategoryId,
                     CategoryName = x.Category.Name,
                     Content = x.Content,
                     CreatedAt = x.CreatedAt,
                     Description = x.Description,
                     Title = x.Title,
                     AccountInfo = new AccountInfoModel
                     {
                         Id = x.AccountId,
                         FirstName = x.Account.FirstName,
                         LastName = x.Account.LastName,
                         ProfilePhoto = x.Account.ProfilePhoto
                     },
                     TagNames = x.BlogTags
                     .Where(x => tagId != null ? x.TagId == tagId : x.Id > 0)
                     .Select(x => new BlogTagModel
                     {
                         TagName = x.Tag.Name,
                         TagId = x.Tag.Id
                     }).ToList()
                 }).OrderByDescending(x => x.Id).ToListAsync());
        }


        public async Task<List<BlogListModel>> GetAllByCategoryIdOrAccountIdAsync(int? categoryId, int? accountId)
        {
            return await _dal.TableNoTracking
                .Where(x => !x.Account.IsBlocked)
                .Where(x => categoryId != null ? x.CategoryId == categoryId : x.Id > 0)
                .Where(x => accountId != null ? x.AccountId == accountId : x.Id > 0)
                .Select(x => new BlogListModel
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    Title = x.Title,
                    AccountInfo = new AccountInfoModel
                    {
                        Id = x.AccountId,
                        FirstName = x.Account.FirstName,
                        LastName = x.Account.LastName,
                        ProfilePhoto = x.Account.ProfilePhoto
                    },
                    TagNames = x.BlogTags.Where(x => x.BlogId == x.Blog.Id).Select(x => new BlogTagModel
                    {
                        TagName = x.Tag.Name,
                        TagId = x.Tag.Id
                    }).ToList(),
                }).OrderByDescending(x => x.Id).ToListAsync();
        }


        public Task<BlogModel> GetAsync(int id)
        {
            throw new NotFoundException(DbMessage.ObsoleteMethod);
        }


        [SecurityAspect]
        [RemoveCacheAspect]
        [ValidationAspect(typeof(BlogValidator))]
        public async Task<IDataResponse<int>> InsertAsync(BlogModel model)
        {
            model.AccountId = _userService.AccountId;
            var result = await _dal.InsertAsync(_mapper.Map<Blog>(model));
            if (!result.Success) return new ErrorDataResponse<int>(result.Message);

            var resultBlogTag = await SaveBlogTags(model.TagIds, result.Data);
            if (!resultBlogTag.Success) return new ErrorDataResponse<int>(result.Message);

            return new SuccessDataResponse<int>(result.Data, result.Message);
        }


        [SecurityAspect]
        [RemoveCacheAspect]
        [ValidationAspect(typeof(BlogValidator))]
        public async Task<IResponse> UpdateAsync(BlogModel model)
        {
            model.AccountId = _userService.AccountId;
            var result = await _dal.UpdateAsync(_mapper.Map<Blog>(model));

            var resultBlogTag = await SaveBlogTags(model.TagIds, model.Id);
            if (!resultBlogTag.Success) return new ErrorDataResponse<int>(result.Message);

            return new SuccessDataResponse<int>(model.Id, result.Message);
        }


        public async Task<IEnumerable<DropdownModel>> SelectListAsync()
        {
            var entities = await _dal.TableNoTracking.OrderByDescending(x => x.Title).ToListAsync();
            return entities.Select(x => new DropdownModel
            {
                Id = x.Id,
                Description = x.Title
            });
        }


        private async Task<IResponse> SaveBlogTags(List<int> Ids, int blogId)
        {
            if (Ids.Count > 0)
            {
                foreach (var item in Ids)
                {
                    var result = await _dalBlogTag.InsertAsync(new BlogTag
                    {
                        BlogId = blogId,
                        TagId = item,
                    });
                    if (!result.Success) return new ErrorResponse();
                }
            }
            return new SuccessResponse();
        }
    }
}
