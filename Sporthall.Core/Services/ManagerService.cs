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
    public class ManagerService : SimpleServiceBase, IManagerService
    {
        public ManagerService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task SetManagerAsync(ManagerInfo managerInfo)
        {
            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.ManagerInfoRepository.Add(managerInfo);
                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.AddToRoleAsync(await UserManager.FindByIdAsync(managerInfo.UserId), AppRoles.Manager);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public async Task SetManagerAsync(ManagerInfo managerInfo, IEnumerable<Hall> halls)
        {
            if (halls is null)
            {
                await SetManagerAsync(managerInfo);
                return;
            }

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.ManagerInfoRepository.Add(managerInfo);
                UnitOfWork.ManagerUserHallRepository.AddRange(halls.Select(h => new ManagerUserHall(h.Id, managerInfo.UserId)));
                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.AddToRoleAsync(await UserManager.FindByIdAsync(managerInfo.UserId), AppRoles.Manager);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public async Task UpdateManagerAsync(ManagerInfo managerInfo)
        {
            UnitOfWork.ManagerInfoRepository.Update(managerInfo);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateManagerAsync(ManagerInfo managerInfo, IEnumerable<Hall> halls)
        {
            if (halls is null)
            {
                await UpdateManagerAsync(managerInfo);
                return;
            }

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.ManagerInfoRepository.Add(managerInfo);
                UnitOfWork.ManagerUserHallRepository.UpdateRange(
                    halls.Select(h => new ManagerUserHall(h.Id, managerInfo.UserId)));
                await SetHallsForManagerAsync(managerInfo.UserId, halls);
                await UnitOfWork.SaveChangesAsync();
            });
        }

        public async Task UnsetManagerAsync(User user)
        {
            await UnitOfWork.TransactionAsync(async () =>
            {
                var managerInfo = await UnitOfWork.ManagerInfoRepository.GetSingleAsync(a => a.UserId == user.Id);
                UnitOfWork.ManagerInfoRepository.Remove(managerInfo);

                await UnitOfWork.SaveChangesAsync();

                var identityRes = await UserManager.RemoveFromRoleAsync(user, AppRoles.Manager);
                ThrowIfIdentityNotSuccess(identityRes);
            });
        }

        public Task<ManagerInfo> GetManagerInfoByUserIdAsync(string managerUserId)
        {
            return UnitOfWork.ManagerInfoRepository.GetSingleAsync(a => a.UserId == managerUserId);
        }

        private async Task SetHallsForManagerAsync(string mangerUserId, IEnumerable<Hall> halls)
        {
            var existManagerUserHallList = await UnitOfWork.ManagerUserHallRepository.
                GetByFilterAsync(a => a.ManagerUserId == mangerUserId);
            var toSetManagerUserHallList = halls.Select(a => new ManagerUserHall(a.Id, mangerUserId));

            var diff = DbHelper.EntitiesDiff(existManagerUserHallList, toSetManagerUserHallList, new ManagerUserHallIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.ManagerUserHallRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.ManagerUserHallRepository.AddRange(diff.ToAddEntities);
            }
        }

        public async Task<IEnumerable<Hall>> GetManagerHallsAsync(string managerUserId)
        {
            return (await UnitOfWork.ManagerUserHallRepository.
                GetByFilterWithIncludesAsync(a => a.ManagerUserId == managerUserId, true, false)).
                Select(a => a.Hall);
        }

        public async Task<User> GetManagerUserAsync(string managerUserId)
        {
            await ThrowIfUserNotInRoleAsync(new User { Id = managerUserId }, AppRoles.Manager);
            return await UserManager.FindByIdAsync(managerUserId);
        }

        public async Task<IEnumerable<User>> GetAllManagerUsersAsync()
        {
            return await UserManager.GetUsersInRoleAsync(AppRoles.Manager);
        }
    }
}
