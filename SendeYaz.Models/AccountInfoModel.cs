using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class AccountInfoModel:IBaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
