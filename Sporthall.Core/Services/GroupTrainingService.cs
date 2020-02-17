using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Helpers;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class GroupTrainingService : SimpleServiceBase, IGroupTrainingService
    {
        public GroupTrainingService(AppUserManager userManager, SignInManager<User> signInManager,
            IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task CreateGroupTrainingAsync(GroupTraining groupTraining)
        {
            UnitOfWork.GroupTrainingRepository.Add(groupTraining);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task CreateGroupTrainingAsync(
            GroupTraining groupTraining,
            IEnumerable<User> coachUsers = null,
            IEnumerable<User> clientUsers = null)
        {
            if (coachUsers is null && clientUsers is null)
            {
                await CreateGroupTrainingAsync(groupTraining);
                return;
            }

            await UnitOfWork.TransactionAsync(async () =>
            {
                await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

                UnitOfWork.GroupTrainingRepository.Add(groupTraining);
                await UnitOfWork.SaveChangesAsync();

                if (coachUsers != null)
                {
                    UnitOfWork.CoachUserGroupTrainingRepository.AddRange(coachUsers.Select(u => new CoachUserGroupTraining(groupTraining.Id, u.Id)));
                    await UnitOfWork.SaveChangesAsync();
                }

                if (clientUsers != null)
                {
                    UnitOfWork.ClientUserGroupTrainingRepository.AddRange(clientUsers.Select(u => new ClientUserGroupTraining(groupTraining.Id, u.Id)));
                    await UnitOfWork.SaveChangesAsync();
                }
            });
        }

        public async Task UpdateGroupTrainingAsync(GroupTraining groupTraining)
        {
            UnitOfWork.GroupTrainingRepository.Update(groupTraining);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateGroupTrainingAsync(GroupTraining groupTraining,
            IEnumerable<User> coachUsers = null,
            IEnumerable<User> clientUsers = null)
        {
            if (coachUsers is null && clientUsers is null)
            {
                await UpdateGroupTrainingAsync(groupTraining);
                return;
            }

            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            UnitOfWork.GroupTrainingRepository.Update(groupTraining);

            if (coachUsers != null)
            {
                await SetCoachUsersToGroupTrainingAsync(groupTraining.Id, coachUsers);
            }

            if (clientUsers != null)
            {
                await SetClientsToGroupTrainingAsync(groupTraining.Id, clientUsers);
            }

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveGroupTrainingAsync(int groupTrainingId)
        {
            UnitOfWork.GroupTrainingRepository.Remove(new GroupTraining { Id = groupTrainingId });
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task AddClientUsersAsync(int groupTrainingId, params User[] clientUsers)
        {
            UnitOfWork.ClientUserGroupTrainingRepository.AddRange(
                clientUsers.Select(u => new ClientUserGroupTraining(groupTrainingId, u.Id)));
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task SetClientUsersAsync(int groupTrainingId, params User[] clientUsers)
        {
            await SetClientsToGroupTrainingAsync(groupTrainingId, clientUsers);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UnsetClientUsersAsync(int groupTrainingId, params User[] clientUsers)
        {
            UnitOfWork.ClientUserGroupTrainingRepository.RemoveRange(clientUsers.Select(a => new ClientUserGroupTraining(groupTrainingId, a.Id)));
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetClientUsersAsync(int groupTrainingId)
        {
            return (await UnitOfWork.ClientUserGroupTrainingRepository.GetAllWithIncludesAsync(false, true)).
                Select(a => a.ClientUser);
        }

        public async Task SetCoachUsersAsync(int groupTrainingId, params User[] coachUsers)
        {
            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            await SetCoachUsersToGroupTrainingAsync(groupTrainingId, coachUsers);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UnsetCoachUsersAsync(int groupTrainingId, params User[] coachUsers)
        {
            UnitOfWork.CoachUserGroupTrainingRepository.RemoveRange(
                coachUsers.Select(a => new CoachUserGroupTraining(groupTrainingId, a.Id)));
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetCoachUsersAsync(int groupTrainingId)
        {
            return (await UnitOfWork.CoachUserGroupTrainingRepository.GetAllWithIncludesAsync(false, true)).
                Select(a => a.CoachUser);
        }

        public Task<GroupTraining> GetGroupTrainingByIdAsync(int groupTrainingId)
        {
            return UnitOfWork.GroupTrainingRepository.GetSingleAsync(a => a.Id == groupTrainingId);
        }

        public Task<IEnumerable<GroupTraining>> GetAllGroupTrainingsAsync()
        {
            return UnitOfWork.GroupTrainingRepository.GetAllAsync();
        }

        public Task<IEnumerable<GroupTraining>> GetGroupTrainingsByFilterAsync(Expression<Func<GroupTraining, bool>> predicate)
        {
            return UnitOfWork.GroupTrainingRepository.GetByFilterAsync(predicate);
        }

        private async Task SetClientsToGroupTrainingAsync(int groupTrainingId, IEnumerable<User> clientUsers)
        {
            var clientGroupTrainingList = await UnitOfWork.ClientUserGroupTrainingRepository.
                GetByFilterAsync(a => a.GroupTrainingId == groupTrainingId);

            var toSetClients = clientUsers.Select(a => new ClientUserGroupTraining(groupTrainingId, a.Id));

            var diff = DbHelper.EntitiesDiff(clientGroupTrainingList, toSetClients, new ClientUserGroupTrainingIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.ClientUserGroupTrainingRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.ClientUserGroupTrainingRepository.AddRange(diff.ToAddEntities);
            }
        }

        private async Task SetCoachUsersToGroupTrainingAsync(int groupTrainingId, IEnumerable<User> coachUsers)
        {
            var coachGroupTrainingList = await UnitOfWork.CoachUserGroupTrainingRepository.
                GetByFilterAsync(a => a.GroupTrainingId == groupTrainingId);

            var toSetCoachUserGroupTrainingList = coachUsers.Select(u => new CoachUserGroupTraining(groupTrainingId, u.Id));

            var diff = DbHelper.EntitiesDiff(coachGroupTrainingList, toSetCoachUserGroupTrainingList, new CoachUserGroupTrainingIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.CoachUserGroupTrainingRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.CoachUserGroupTrainingRepository.AddRange(diff.ToAddEntities);
            }
        }

        public async Task<IEnumerable<GroupTraining>> GetGroupTrainingsByClientIdAsync(string clientUserId)
        {
            return (await UnitOfWork.ClientUserGroupTrainingRepository.
                GetByFilterWithIncludesAsync(a => a.ClientUserId == clientUserId, true, false)).
                Select(a => a.GroupTraining);
        }

        public Task<IEnumerable<GroupTraining>> GetActiveGroupTrainings()
        {
            return GetGroupTrainingsByFilterAsync(a => a.Active);
        }

        public async Task<IEnumerable<GroupTraining>> GetUserGroupTrainings(string userId)
        {
            return (await UnitOfWork.ClientUserGroupTrainingRepository.GetByFilterWithIncludesAsync(a => a.ClientUserId == userId, true, false)).
                Select(a => a.GroupTraining);
        }
    }
}
