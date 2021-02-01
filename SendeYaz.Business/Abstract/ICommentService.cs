using AutoMapper;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface ICommentService : IServiceRepository<CommentModel>
    {
        Task<List<CommentListModel>> GetAllWithSubCommentsAsync(int blogId, int? parentId);
    }
}
