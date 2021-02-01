using SendeYaz.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Plugins.Authentication.Models
{
    public class AccountRule
    {
        public ApplicationModule ApplicationModule { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
