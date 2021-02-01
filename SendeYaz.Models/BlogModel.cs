using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class BlogModel:IBaseModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public List<int> TagIds { get; set; }
    }

    public class BlogListModel:IBaseModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountInfoModel AccountInfo { get; set; }
        public List<BlogTagModel> TagNames { get; set; }
    }
}
