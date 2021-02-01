using SendeYaz.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Repositories
{
    public interface IDataAccessRepository<TEntity>:IRepository<TEntity> where TEntity : class,IBaseEntity,new()
    {
    }
}
