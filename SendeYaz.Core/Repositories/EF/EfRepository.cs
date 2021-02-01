﻿using Microsoft.EntityFrameworkCore;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Signatures;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Core.Repositories.EF
{
    public class EfRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IBaseEntity, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private DbSet<TEntity> _entities;

        public EfRepository(TContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entities.AnyAsync(filter);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await Entities.FindAsync(id) != null;
        }

        public async Task<IDataResponse<int>> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorDataResponse<int>(DbMessage.DataNotFound);

            try
            {
                Entities.Remove(entity);
                await SaveChangesAsync();
                return new SuccessDataResponse<int>(entity.Id, DbMessage.DataRemoved);
            }
            catch (Exception e)
            {
                return new ErrorDataResponse<int>(entity.Id, e.Message);
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entities.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public IEnumerable<TEntity> GetSql(string sql)
        {
            return Entities.FromSqlRaw(sql);
        }
        public async Task<IDataResponse<int>> InsertAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorDataResponse<int>(DbMessage.DataNotFound);

            try
            {
                Entities.Add(entity);
                await SaveChangesAsync();
                return new SuccessDataResponse<int>(entity.Id, DbMessage.DataInserted);
            }
            catch (Exception e)
            {
                return new ErrorDataResponse<int>(e.Message);
            }
        }

        public async Task<IResponse> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorResponse(DbMessage.DataNotFound);
            try
            {
                var local = _context.Set<TEntity>().Local.FirstOrDefault(entry => entry.Id.Equals(entity.Id));
                if (local != null)
                    _context.Entry(local).State = EntityState.Detached;
                Entities.Update(entity);
                await SaveChangesAsync();
                return new SuccessResponse(DbMessage.DataUpdated);
            }
            catch (DbUpdateException e)
            {
                return new ErrorResponse(GetFullError(e));
            }
            catch (Exception e)
            {
                return new ErrorResponse($"{e.Message} {e.InnerException}");
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(GetFullError(e));
            }
        }
        private string GetFullError(DbUpdateException e)
        {
            var entries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var exception = e.ToString();
            if (exception.Contains("The DELETE statement conflicted with the REFERENCE"))
                exception = "Bu kayıt silinemez. Önce alt kayıtları silin.";
            return exception;
        }
        private DbSet<TEntity> Entities => _entities ??= _context.Set<TEntity>();
        public IQueryable<TEntity> Table => Entities;
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
    }
}
