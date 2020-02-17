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
    public class EfCoachUserGroupTrainingRepository :
        EfRepository<CoachUserGroupTraining>,
        IRepository<CoachUserGroupTraining>,
        ICoachUserGroupTrainingRepository
    {
        public EfCoachUserGroupTrainingRepository(EfDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<CoachUserGroupTraining> GetAllWithIncludes(
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            IQueryable<CoachUserGroupTraining> query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return query.AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<CoachUserGroupTraining>> GetAllWithIncludesAsync(
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            var query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<CoachUserGroupTraining> GetByFilterWithIncludes(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            var query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return query.Where(predicate).AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<CoachUserGroupTraining>> GetByFilterWithIncludesAsync(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            var query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }

        public CoachUserGroupTraining GetSingleWithIncludes(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            var query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return query.SingleOrDefault(predicate);
        }

        public Task<CoachUserGroupTraining> GetSingleWithIncludesAsync(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            var query = DbContext.CoachUserGroupTraining.AsQueryable();
            IncludeProperties(ref query, includeGroupTraining, includeCoachUser);
            return query.SingleOrDefaultAsync(predicate);
        }

        private void IncludeProperties(
            ref IQueryable<CoachUserGroupTraining> query,
            bool includeGroupTraining,
            bool includeCoachUser)
        {
            if (includeGroupTraining)
            {
                query = query.Include(a => a.GroupTraining);
            }

            if (includeCoachUser)
            {
                query = query.Include(a => a.CoachUser);
            }
        }
    }
}
