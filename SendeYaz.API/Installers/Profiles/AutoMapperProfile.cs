using AutoMapper;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendeYaz.API.Installers.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountModel>();
            CreateMap<AccountModel, Account>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<Rule, RuleModel>();
            CreateMap<RuleModel, Rule>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<Tag, TagModel>();
            CreateMap<TagModel, Tag>();

            CreateMap<Blog, BlogModel>();
            CreateMap<BlogModel, Blog>();

            CreateMap<Blog, BlogListModel>();
            CreateMap<BlogListModel, Blog>();

            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();

            CreateMap<Comment, CommentListModel>();
            CreateMap<CommentListModel, Comment>();
        }
    }
}
