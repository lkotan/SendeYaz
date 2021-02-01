using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Entities
{
    public class Category:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
