using Microsoft.EntityFrameworkCore;
using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfClientUserGroupTrainingRepository :
        EfRepository<ClientUserGroupTraining>,
        IRepository<ClientUserGroupTraining>,
        IClientUserGroupTrainingRepository
    {
        public EfClientUserGroupTrainingRepository(EfDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ClientUserGroupTraining> GetAllWithIncludes(
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeClientUser);
            return query.AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<ClientUserGroupTraining>> GetAllWithIncludesAsync(
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeClientUser);
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<ClientUserGroupTraining> GetByFilterWithIncludes(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.Where(predicate);

            IncludeProperties(ref query, includeGroupTraining, includeClientUser);

            return query.AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<ClientUserGroupTraining>> GetByFilterWithIncludesAsync(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.Where(predicate);
            IncludeProperties(ref query, includeGroupTraining, includeClientUser);
            return await query.AsNoTracking().ToListAsync();
        }

        public ClientUserGroupTraining GetSingleWithIncludes(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeClientUser);
            return query.SingleOrDefault(predicate);
        }

        public Task<ClientUserGroupTraining> GetSingleWithIncludesAsync(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser)
        {
            IQueryable<ClientUserGroupTraining> query = DbContext.ClientUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeClientUser);
            return query.SingleOrDefaultAsync(predicate);
        }

        private void IncludeProperties(ref IQueryable<ClientUserGroupTraining> query,
            bool includeGroupTraining, bool includeClientUser)
        {
            if (includeGroupTraining)
            {
                query = query.Include(a => a.GroupTraining);
            }

            if (includeClientUser)
            {
                query = query.Include(a => a.ClientUser);
            }
        }
    }
}
