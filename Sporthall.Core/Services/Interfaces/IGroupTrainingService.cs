using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IGroupTrainingService
    {
        Task CreateGroupTrainingAsync(GroupTraining groupTraining);

        Task CreateGroupTrainingAsync(GroupTraining groupTraining, IEnumerable<User> coachUsers, IEnumerable<User> clientUsers);

        Task UpdateGroupTrainingAsync(GroupTraining groupTraining);

        Task UpdateGroupTrainingAsync(GroupTraining groupTraining, IEnumerable<User> coachUsers, IEnumerable<User> clientUsers);

        Task RemoveGroupTrainingAsync(int groupTrainingId);

        Task SetCoachUsersAsync(int groupTrainingId, params User[] coachUsers);

        Task<IEnumerable<User>> GetCoachUsersAsync(int groupTrainingId);

        Task UnsetCoachUsersAsync(int groupTrainingId, params User[] coachUsers);

        Task AddClientUsersAsync(int groupTrainingId, params User[] clientUsers);

        Task SetClientUsersAsync(int groupTrainingId, params User[] clientUsers);

        Task<IEnumerable<User>> GetClientUsersAsync(int groupTrainingId);

        Task UnsetClientUsersAsync(int groupTrainingId, params User[] clientUsers);

        Task<GroupTraining> GetGroupTrainingByIdAsync(int groupTrainingId);

        Task<IEnumerable<GroupTraining>> GetAllGroupTrainingsAsync();

        Task<IEnumerable<GroupTraining>> GetGroupTrainingsByFilterAsync(Expression<Func<GroupTraining, bool>> predicate);

        Task<IEnumerable<GroupTraining>> GetActiveGroupTrainings();

        Task<IEnumerable<GroupTraining>> GetGroupTrainingsByClientIdAsync(string clientUserId);

        Task<IEnumerable<GroupTraining>> GetUserGroupTrainings(string userId);
    }
}
