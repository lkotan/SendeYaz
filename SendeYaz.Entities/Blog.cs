using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Entities
{
    public class Blog:IBaseEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Category Category { get; set; }
        public Account Account { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
