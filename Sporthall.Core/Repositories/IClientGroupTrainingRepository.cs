using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Repositories
{
    public interface IClientUserGroupTrainingRepository : IRepository<ClientUserGroupTraining>
    {
        IEnumerable<ClientUserGroupTraining> GetAllWithIncludes(
            bool includeGroupTraining,
            bool includeClientUser
         );

        Task<IEnumerable<ClientUserGroupTraining>> GetAllWithIncludesAsync(
            bool includeGroupTraining,
            bool includeClientUser
        );

        IEnumerable<ClientUserGroupTraining> GetByFilterWithIncludes(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser
        );

        Task<IEnumerable<ClientUserGroupTraining>> GetByFilterWithIncludesAsync(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser
        );

        ClientUserGroupTraining GetSingleWithIncludes(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser
        );

        Task<ClientUserGroupTraining> GetSingleWithIncludesAsync(
            Expression<Func<ClientUserGroupTraining, bool>> predicate,
            bool includeGroupTraining,
            bool includeClientUser
        );
    }
}
