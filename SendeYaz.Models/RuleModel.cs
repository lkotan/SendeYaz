using SendeYaz.Core.Enums;
using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Models
{
    public class RuleModel:IBaseModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public ApplicationModule ApplicationModule { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
    public class RuleListModel
    {
        public int Id { get; set; }
        public ApplicationModule ApplicationModule { get; set; }
        public string ModuleName { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
