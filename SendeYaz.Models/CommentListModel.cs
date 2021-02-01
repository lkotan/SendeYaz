using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class CommentListModel:IBaseModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentListModel> SubComments { get; set; }
    }
}
