using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories
{
    public interface ICoachUserHallRepository : IRepository<CoachUserHall>
    {
        IEnumerable<CoachUserHall> GetAllWithIncludes(
            bool includeHall,
            bool includeCoachUser
        );

        ValueTask<IEnumerable<CoachUserHall>> GetAllWithIncludesAsync(
            bool includeHall,
            bool includeCoachUser
        );

        IEnumerable<CoachUserHall> GetByFilterWithIncludes(
            Expression<Func<CoachUserHall, bool>> predicate,
            bool includeHall,
            bool includeCoachUser
        );

        Task<IEnumerable<CoachUserHall>> GetByFilterWithIncludesAsync(
            Expression<Func<CoachUserHall, bool>> predicate,
            bool includeHall,
            bool includeCoachUser
        );

        CoachUserHall GetSingleWithIncludes(
            Expression<Func<CoachUserHall, bool>> predicate,
            bool includeHall,
            bool includeCoachUser
        );

        Task<CoachUserHall> GetSingleWithIncludesAsync(
            Expression<Func<CoachUserHall, bool>> predicate,
            bool includeHall,
            bool includeCoachUser
        );
    }
}
