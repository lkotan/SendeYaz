using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Entities
{
    public class Tag:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
