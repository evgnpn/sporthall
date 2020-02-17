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
    public class SoloTrainingService : SimpleServiceBase, ISoloTrainingService
    {
        public SoloTrainingService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task CreateSoloTrainingAsync(SoloTraining soloTraining)
        {
            UnitOfWork.SoloTrainingRepository.Add(soloTraining);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task CreateSoloTrainingAsync(SoloTraining soloTraining, IEnumerable<User> coachUsers = null)
        {
            if (coachUsers is null || coachUsers.Count() < 1)
            {
                await CreateSoloTrainingAsync(soloTraining);
                return;
            }

            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.SoloTrainingRepository.Add(soloTraining);
                await UnitOfWork.SaveChangesAsync();


                UnitOfWork.CoachUserSoloTrainingRepository.AddRange(
                    coachUsers.Select(u => new CoachUserSoloTraining(soloTraining.Id, u.Id)));
                await UnitOfWork.SaveChangesAsync();

            });
        }

        public async Task UpdateSoloTrainingAsync(SoloTraining soloTraining)
        {
            UnitOfWork.SoloTrainingRepository.Update(soloTraining);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSoloTrainingAsync(SoloTraining soloTraining, IEnumerable<User> coachUsers)
        {
            if (coachUsers is null || coachUsers.Count() < 1)
            {
                await UpdateSoloTrainingAsync(soloTraining);
                return;
            }

            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            UnitOfWork.SoloTrainingRepository.Update(soloTraining);
            await SetCoachUsersToSoloTrainingAsync(soloTraining.Id, coachUsers);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveSoloTrainingAsync(int soloTrainingId)
        {
            UnitOfWork.SoloTrainingRepository.Remove(new SoloTraining { Id = soloTrainingId });
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task SetClientUserAsync(int soloTrainingId, string userId)
        {
            SoloTraining soloTraining = await UnitOfWork.SoloTrainingRepository.GetSingleAsync(a => a.Id == soloTrainingId);
            soloTraining.ClientUserId = userId;
            UnitOfWork.SoloTrainingRepository.Update(soloTraining);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UnsetClientUserAsync(int soloTrainingId)
        {
            await SetClientUserAsync(soloTrainingId, null);
        }

        public async Task<User> GetClientUserAsync(int soloTrainingId)
        {
            var soloTraining = await UnitOfWork.SoloTrainingRepository.
                GetSingleAsync(a => a.Id == soloTrainingId);
            var clientUser = await UnitOfWork.UserRepository.
                GetSingleAsync(a => a.Id == soloTraining.ClientUserId);
            return clientUser;
        }

        public async Task SetCoachUsersAsync(int soloTrainingId, params User[] coachUsers)
        {
            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            await SetCoachUsersToSoloTrainingAsync(soloTrainingId, coachUsers);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UnsetCoachUsersAsync(int soloTrainingId, params User[] coachUsers)
        {
            UnitOfWork.CoachUserSoloTrainingRepository.RemoveRange(
                coachUsers.Select(u => new CoachUserSoloTraining(soloTrainingId, u.Id)));
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetCoachUsersAsync(int soloTrainingId)
        {
            var coachUsers = (await UnitOfWork.CoachUserSoloTrainingRepository.
                 GetByFilterWithIncludesAsync(a => a.SoloTrainingId == soloTrainingId, false, true)).
                 Select(a => a.CoachUser);
            return coachUsers;
        }

        public Task<SoloTraining> GetSoloTrainingByIdAsync(int soloTrainingId)
        {
            return UnitOfWork.SoloTrainingRepository.
                GetSingleAsync(a => a.Id == soloTrainingId);
        }

        public Task<IEnumerable<SoloTraining>> GetAllSoloTrainingsAsync()
        {
            return UnitOfWork.SoloTrainingRepository.
                GetAllAsync();
        }

        public Task<IEnumerable<SoloTraining>> GetSoloTrainingsByFilterAsync(Expression<Func<SoloTraining, bool>> predicate)
        {
            return UnitOfWork.SoloTrainingRepository.GetByFilterAsync(predicate);
        }

        public async Task<Hall> GetSoloTrainingHallAsync(SoloTraining soloTraining)
        {
            return await UnitOfWork.HallRepository.GetSingleAsync(h => h.Id == soloTraining.HallId);
        }

        public Task<IEnumerable<SoloTraining>> GetActiveSoloTrainings()
        {
            return GetSoloTrainingsByFilterAsync(a => a.Active);
        }

        public Task<IEnumerable<SoloTraining>> GetUserSoloTrainings(string userId)
        {
            return UnitOfWork.SoloTrainingRepository.GetByFilterAsync(a => a.ClientUserId == userId);
        }

        private async Task SetCoachUsersToSoloTrainingAsync(int soloTrainingId, IEnumerable<User> coachUsers)
        {
            var existsCoachUserSoloTrainingList = await UnitOfWork.CoachUserSoloTrainingRepository.
                GetByFilterAsync(a => a.SoloTrainingId == soloTrainingId);
            var toSetCoachUserSoloTrainingList = coachUsers.Select(a => new CoachUserSoloTraining(soloTrainingId, a.Id));

            var diff = DbHelper.EntitiesDiff(existsCoachUserSoloTrainingList,
                toSetCoachUserSoloTrainingList, new CoachUserSoloTrainingIdComparer());

            UnitOfWork.CoachUserSoloTrainingRepository.RemoveRange(diff.ToRemoveEntities);
            UnitOfWork.CoachUserSoloTrainingRepository.AddRange(diff.ToAddEntities);
        }
    }
}
