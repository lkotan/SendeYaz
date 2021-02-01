using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class CategoryModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
