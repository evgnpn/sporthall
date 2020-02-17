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
    public class EfCoachUserSoloTrainingRepository :
        EfRepository<CoachUserSoloTraining>, IRepository<CoachUserSoloTraining>, ICoachUserSoloTrainingRepository
    {
        public EfCoachUserSoloTrainingRepository(EfDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<CoachUserSoloTraining> GetAllWithIncludes(bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return query.AsNoTracking().AsEnumerable();
        }

        public async ValueTask<IEnumerable<CoachUserSoloTraining>> GetAllWithIncludesAsync(bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<CoachUserSoloTraining> GetByFilterWithIncludes(Expression<Func<CoachUserSoloTraining, bool>> predicate, bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return query.Where(predicate).AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<CoachUserSoloTraining>> GetByFilterWithIncludesAsync(Expression<Func<CoachUserSoloTraining, bool>> predicate, bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }

        public CoachUserSoloTraining GetSingleWithIncludes(Expression<Func<CoachUserSoloTraining, bool>> predicate, bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return query.SingleOrDefault(predicate);
        }

        public Task<CoachUserSoloTraining> GetSingleWithIncludesAsync(Expression<Func<CoachUserSoloTraining, bool>> predicate, bool includeSoloTraining = true, bool includeCoachUser = true)
        {
            IQueryable<CoachUserSoloTraining> query = DbContext.CoachUserSoloTraining.AsQueryable();
            IncludeProperties(ref query, includeSoloTraining, includeCoachUser);
            return query.SingleOrDefaultAsync(predicate);
        }

        private void IncludeProperties(ref IQueryable<CoachUserSoloTraining> query, in bool includeSoloTraining, in bool includeCoachUser)
        {
            if (includeSoloTraining)
            {
                query = query.Include(a => a.SoloTraining);
            }

            if (includeCoachUser)
            {
                query = query.Include(a => a.CoachUser);
            }
        }
    }
}
