using SendeYaz.Core.Enums;
using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Models
{
    public class AccountRulesModel
    {
        public ApplicationModule ApplicationModule { get; set; }
        public string ApplicationModuleName { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
