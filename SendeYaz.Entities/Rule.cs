using SendeYaz.Core.Enums;
using SendeYaz.Core.Signatures;

namespace SendeYaz.Entities
{
    public class Rule:IBaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public ApplicationModule ApplicationModule { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public Role Role { get; set; }
    }
}
