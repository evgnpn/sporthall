using Microsoft.EntityFrameworkCore;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Infrastructure.Repositories.EntityFramework.Base
{
    public abstract class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected EfDbContext DbContext { get; private set; }

        public EfRepository(EfDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DbContext.AddRange(entities);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsNoTracking().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAllRange(int skipCount, int takeCount)
        {
            return DbContext.Set<TEntity>().Skip(skipCount).Take(takeCount).AsNoTracking().AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllRangeAsync(int skipCount, int takeCount)
        {
            return await DbContext.Set<TEntity>().Skip(skipCount).Take(takeCount).AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).AsNoTracking().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetByFilterRange(Expression<Func<TEntity, bool>> predicate, int skipCount, int takeCount)
        {
            return DbContext.Set<TEntity>().Where(predicate).Skip(skipCount).Take(takeCount).AsNoTracking().AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByFilterRangeAsync(Expression<Func<TEntity, bool>> predicate, int skipCount, int takeCount)
        {
            return await DbContext.Set<TEntity>().Where(predicate).Skip(skipCount).Take(takeCount).AsNoTracking().ToListAsync();
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public virtual void Remove(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbContext.UpdateRange(entities);
        }

        public int GetAllCount(int count)
        {
            return DbContext.Set<TEntity>().Count();
        }

        public Task<int> GetAllCountAsync(int count)
        {
            return DbContext.Set<TEntity>().CountAsync();
        }

        public int GetByFilterCount(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).Count();
        }

        public Task<int> GetByFilterCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).CountAsync();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
