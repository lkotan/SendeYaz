using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Entities
{
    public class Comment:IBaseEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public List<Comment> SubComments { get; set; }

        public Blog Blog { get; set; }

    }
}
