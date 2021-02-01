using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class TagModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
