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
    public class EfCoachUserHallRepository : EfRepository<CoachUserHall>, IRepository<CoachUserHall>, ICoachUserHallRepository
    {
        public EfCoachUserHallRepository(EfDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<CoachUserHall> GetAllWithIncludes(bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            return query.AsNoTracking().AsEnumerable();
        }

        public async ValueTask<IEnumerable<CoachUserHall>> GetAllWithIncludesAsync(bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<CoachUserHall> GetByFilterWithIncludes(Expression<Func<CoachUserHall, bool>> predicate, bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            return query.Where(predicate).AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<CoachUserHall>> GetByFilterWithIncludesAsync(Expression<Func<CoachUserHall, bool>> predicate, bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }

        public CoachUserHall GetSingleWithIncludes(Expression<Func<CoachUserHall, bool>> predicate, bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            return query.SingleOrDefault(predicate);
        }

        public async Task<CoachUserHall> GetSingleWithIncludesAsync(Expression<Func<CoachUserHall, bool>> predicate, bool includeHall, bool includeCoachUser)
        {
            IQueryable<CoachUserHall> query = DbContext.CoachUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeCoachUser);
            Func<CoachUserHall, bool> we = predicate.Compile();
            return await query.SingleOrDefaultAsync(a => we.Invoke(a));
        }

        private void IncludeProperties(ref IQueryable<CoachUserHall> query, in bool includeHall, in bool includeCoachUser)
        {
            if (includeHall)
            {
                query = query.Include(a => a.Hall);
            }

            if (includeCoachUser)
            {
                query = query.Include(a => a.CoachUser);
            }
        }
    }
}
