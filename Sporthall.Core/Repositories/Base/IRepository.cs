using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllRange(int skipCount, int takeCount);

        Task<IEnumerable<TEntity>> GetAllAsync();

        int GetAllCount(int count);

        Task<IEnumerable<TEntity>> GetAllRangeAsync(int skipCount, int takeCount);

        Task<int> GetAllCountAsync(int count);

        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> predicate);

        int GetByFilterCount(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetByFilterRange(Expression<Func<TEntity, bool>> predicate, int skipCount, int takeCount);

        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetByFilterCountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetByFilterRangeAsync(Expression<Func<TEntity, bool>> predicate, int skipCount, int takeCount);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        public int SaveChanges();

        public Task<int> SaveChangesAsync();
    }
}
