using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Entities
{
    public class BlogTag:IBaseEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }

        public Blog Blog { get; set; }
        public Tag Tag { get; set; }
    }
}
