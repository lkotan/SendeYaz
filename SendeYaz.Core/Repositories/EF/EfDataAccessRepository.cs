using Microsoft.EntityFrameworkCore;
using SendeYaz.Core.Signatures;

namespace SendeYaz.Core.Repositories.EF
{
    public class EfDataAccessRepository<TEntity> : EfRepository<TEntity, DbContext>, IDataAccessRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        public EfDataAccessRepository(DbContext context) : base(context)
        {
        }
    }
}
