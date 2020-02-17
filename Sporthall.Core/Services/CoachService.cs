using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Helpers;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class CoachService : SimpleServiceBase, ICoachService
    {
        public CoachService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task SetCoachAsync(User user, CoachInfo coachInfo)
        {
            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.CoachInfoRepository.Add(coachInfo);
                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.AddToRoleAsync(user, AppRoles.Coach);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public async Task SetCoachAsync(User user, CoachInfo coachInfo, IEnumerable<Hall> halls)
        {
            if (halls is null)
            {
                await SetCoachAsync(user, coachInfo);
                return;
            }

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.CoachInfoRepository.Add(coachInfo);
                UnitOfWork.CoachUserHallRepository.AddRange(halls.Select(h => new CoachUserHall(h.Id, user.Id)));
                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.AddToRoleAsync(user, AppRoles.Coach);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public async Task UpdateCoachAsync(User user, CoachInfo coachInfo)
        {
            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.CoachInfoRepository.Update(coachInfo);
                await UnitOfWork.SaveChangesAsync();
            });
        }

        public async Task UpdateCoachAsync(User user, CoachInfo coachInfo, IEnumerable<Hall> halls)
        {
            if (halls is null)
            {
                await UpdateCoachAsync(user, coachInfo);
                return;
            }

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.CoachInfoRepository.Add(coachInfo);
                UnitOfWork.CoachUserHallRepository.UpdateRange(halls.Select(h => new CoachUserHall(h.Id, user.Id)));
                await SetHallsForCoachAsync(user.Id, halls);
                await UnitOfWork.SaveChangesAsync();
            });
        }

        public async Task UnsetCoachAsync(User user)
        {
            await UnitOfWork.TransactionAsync(async () =>
            {
                var coachInfo = await UnitOfWork.CoachInfoRepository.GetSingleAsync(a => a.UserId == user.Id);
                UnitOfWork.CoachInfoRepository.Remove(coachInfo);

                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.RemoveFromRoleAsync(new User { Id = user.Id }, AppRoles.Manager);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public Task<CoachInfo> GetCoachInfoByUserIdAsync(string coachUserId)
        {
            return UnitOfWork.CoachInfoRepository.GetSingleAsync(a => a.UserId == coachUserId);
        }

        private async Task SetHallsForCoachAsync(string coachUserId, IEnumerable<Hall> halls)
        {
            var existManagerUserHallList = await UnitOfWork.CoachUserHallRepository.
                GetByFilterAsync(a => a.CoachUserId == coachUserId);
            var toSetManagerUserHallList = halls.Select(a => new CoachUserHall(a.Id, coachUserId));

            var diff = DbHelper.EntitiesDiff(existManagerUserHallList, toSetManagerUserHallList, new CoachUserHallIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.CoachUserHallRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.CoachUserHallRepository.AddRange(diff.ToAddEntities);
            }
        }

        public async Task<IEnumerable<Hall>> GetCoachHallsAsync(string coachUserId)
        {
            return (await UnitOfWork.CoachUserHallRepository.
                GetByFilterWithIncludesAsync(a => a.CoachUserId == coachUserId, true, false)).Select(a => a.Hall);
        }

        public async Task<User> GetCoachUserAsync(string coachUserId)
        {
            await ThrowIfUserNotInRoleAsync(new User { Id = coachUserId }, AppRoles.Coach);
            return await UserManager.FindByIdAsync(coachUserId);
        }

        public async Task<IEnumerable<SoloTraining>> GetCoachSoloTrainingsAsync(string coachUserId)
        {
            await ThrowIfUserNotInRoleAsync(new User { Id = coachUserId }, AppRoles.Coach);
            return (await UnitOfWork.CoachUserSoloTrainingRepository.
                GetByFilterWithIncludesAsync(a => a.CoachUserId == coachUserId, true, false)).
                Select(a => a.SoloTraining);
        }

        public async Task<IEnumerable<GroupTraining>> GetCoachGroupTrainingsAsync(string coachUserId)
        {
            await ThrowIfUserNotInRoleAsync(new User { Id = coachUserId }, AppRoles.Coach);
            return (await UnitOfWork.CoachUserGroupTrainingRepository.
                GetByFilterWithIncludesAsync(a => a.CoachUserId == coachUserId, true, false)).
                Select(a => a.GroupTraining);
        }

        public async Task<IEnumerable<User>> GetAllCoachUsersAsync()
        {
            return await UserManager.GetUsersInRoleAsync(AppRoles.Coach);
        }
    }
}
