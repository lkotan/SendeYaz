using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Models
{
    public class DropdownModel:IBaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
