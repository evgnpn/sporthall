using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories
{
    public interface ICoachUserSoloTrainingRepository : IRepository<CoachUserSoloTraining>
    {
        IEnumerable<CoachUserSoloTraining> GetAllWithIncludes(
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );

        ValueTask<IEnumerable<CoachUserSoloTraining>> GetAllWithIncludesAsync(
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );

        IEnumerable<CoachUserSoloTraining> GetByFilterWithIncludes(
            Expression<Func<CoachUserSoloTraining, bool>> predicate,
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );

        Task<IEnumerable<CoachUserSoloTraining>> GetByFilterWithIncludesAsync(
            Expression<Func<CoachUserSoloTraining, bool>> predicate,
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );

        CoachUserSoloTraining GetSingleWithIncludes(
            Expression<Func<CoachUserSoloTraining, bool>> predicate,
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );

        Task<CoachUserSoloTraining> GetSingleWithIncludesAsync(
            Expression<Func<CoachUserSoloTraining, bool>> predicate,
            bool includeSoloTraining = true,
            bool includeCoachUser = true
        );
    }
}
