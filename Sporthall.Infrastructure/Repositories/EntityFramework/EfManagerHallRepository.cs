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
    public class EfManagerUserHallRepository : EfRepository<ManagerUserHall>, IRepository<ManagerUserHall>, IManagerUserHallRepository
    {
        public EfManagerUserHallRepository(EfDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ManagerUserHall> GetAllWithIncludes(bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return query.AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<ManagerUserHall>> GetAllWithIncludesAsync(bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return await query.AsNoTracking().ToListAsync();
        }

        public IEnumerable<ManagerUserHall> GetByFilterWithIncludes(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return query.Where(predicate).AsNoTracking().AsEnumerable();
        }

        public async Task<IEnumerable<ManagerUserHall>> GetByFilterWithIncludesAsync(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }

        public ManagerUserHall GetSingleWithIncludes(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return query.SingleOrDefault(predicate);
        }

        public Task<ManagerUserHall> GetSingleWithIncludesAsync(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager)
        {
            var query = DbContext.ManagerUserHall.AsQueryable();
            IncludeProperties(ref query, includeHall, includeManager);
            return query.SingleOrDefaultAsync(predicate);
        }

        private void IncludeProperties(ref IQueryable<ManagerUserHall> query, in bool includeHall, in bool includeManager)
        {
            if (includeHall)
            {
                query = query.Include(a => a.Hall);
            }

            if (includeManager)
            {
                query = query.Include(a => a.ManagerUser);
            }
        }
    }
}
