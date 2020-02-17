using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories
{
    public interface IManagerUserHallRepository : IRepository<ManagerUserHall>
    {
        IEnumerable<ManagerUserHall> GetAllWithIncludes(bool includeHall, bool includeManager);

        Task<IEnumerable<ManagerUserHall>> GetAllWithIncludesAsync(bool includeHall, bool includeManager);

        IEnumerable<ManagerUserHall> GetByFilterWithIncludes(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager);

        Task<IEnumerable<ManagerUserHall>> GetByFilterWithIncludesAsync(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager);

        ManagerUserHall GetSingleWithIncludes(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager);

        Task<ManagerUserHall> GetSingleWithIncludesAsync(Expression<Func<ManagerUserHall, bool>> predicate, bool includeHall, bool includeManager);
    }
}
