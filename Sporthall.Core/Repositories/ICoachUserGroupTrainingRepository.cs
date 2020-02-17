using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories
{
    public interface ICoachUserGroupTrainingRepository : IRepository<CoachUserGroupTraining>
    {
        IEnumerable<CoachUserGroupTraining> GetAllWithIncludes(
            bool includeGroupTraining,
            bool includeCoachUser
        );

        Task<IEnumerable<CoachUserGroupTraining>> GetAllWithIncludesAsync(
            bool includeGroupTraining,
            bool includeCoachUser
        );

        IEnumerable<CoachUserGroupTraining> GetByFilterWithIncludes(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser
        );

        Task<IEnumerable<CoachUserGroupTraining>> GetByFilterWithIncludesAsync(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser
        );

        CoachUserGroupTraining GetSingleWithIncludes(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser
        );

        Task<CoachUserGroupTraining> GetSingleWithIncludesAsync(
            Expression<Func<CoachUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeCoachUser
        );
    }
}
