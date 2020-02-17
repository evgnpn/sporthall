using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Comparers;
using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Joins;
using Sporthall.Core.Helpers;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class HallService : SimpleServiceBase, IHallService
    {
        public HallService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task CreateHallAsync(Hall hall)
        {
            UnitOfWork.HallRepository.Add(hall);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task CreateHallAsync(Hall hall, IEnumerable<ISchedule> hallSchedules = null, IEnumerable<User> managerUsers = null, IEnumerable<User> coachUsers = null)
        {
            if (hallSchedules is null && managerUsers is null && coachUsers is null)
            {
                await CreateHallAsync(hall);
                return;
            }

            await ThrowIfUsersNotInRoleAsync(managerUsers, AppRoles.Manager);
            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            await UnitOfWork.TransactionAsync(async () =>
            {
                UnitOfWork.HallRepository.Add(hall);
                await UnitOfWork.SaveChangesAsync();

                if (hallSchedules != null)
                {
                    UnitOfWork.HallScheduleRepository.AddRange(
                        hallSchedules.Select(s => new HallSchedule(hall.Id, s)));
                    await UnitOfWork.SaveChangesAsync();
                }

                if (managerUsers != null)
                {
                    UnitOfWork.ManagerUserHallRepository.AddRange(
                        managerUsers.Select(u => new ManagerUserHall(hall.Id, u.Id)));
                    await UnitOfWork.SaveChangesAsync();
                }

                if (coachUsers != null)
                {
                    UnitOfWork.CoachUserHallRepository.AddRange(
                        coachUsers.Select(u => new CoachUserHall(hall.Id, u.Id)));
                    await UnitOfWork.SaveChangesAsync();
                }
            });
        }

        public async Task UpdateHallAsync(Hall hall)
        {
            UnitOfWork.HallRepository.Update(hall);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateHallAsync(Hall hall, IEnumerable<ISchedule> hallSchedules = null, IEnumerable<User> managerUsers = null, IEnumerable<User> coachUsers = null)
        {
            if (hallSchedules is null && managerUsers is null && coachUsers is null)
            {
                await UpdateHallAsync(hall);
                return;
            }

            await ThrowIfUsersNotInRoleAsync(managerUsers, AppRoles.Manager);
            await ThrowIfUsersNotInRoleAsync(coachUsers, AppRoles.Coach);

            UnitOfWork.HallRepository.Update(hall);

            if (hallSchedules != null)
            {
                await SetSchedulesToHallAsync(hall.Id, hallSchedules);
            }

            if (managerUsers != null)
            {
                await SetManagersToHallAsync(hall.Id, managerUsers);
            }

            if (coachUsers != null)
            {
                await SetCoachUsersToHallAsync(hall.Id, coachUsers);
            }

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveHallAsync(int hallId)
        {
            UnitOfWork.HallRepository.Remove(new Hall { Id = hallId });
            await UnitOfWork.SaveChangesAsync();
        }

        public Task<Hall> GetHallByIdAsync(int hallId)
        {
            return UnitOfWork.HallRepository.GetSingleAsync(a => a.Id == hallId);
        }

        public Task<IEnumerable<Hall>> GetAllHallsAsync()
        {
            return UnitOfWork.HallRepository.GetAllAsync();
        }

        public Task<IEnumerable<Hall>> GetHallsByFilterAsync(Func<Hall, bool> predicate)
        {
            return UnitOfWork.HallRepository.GetByFilterAsync(a => predicate(a));
        }

        public Task<IEnumerable<HallSchedule>> GetHallSchedulesAsync(int hallId)
        {
            return UnitOfWork.HallScheduleRepository.GetByFilterAsync(a => a.HallId == hallId);
        }

        public Task<IEnumerable<SoloTraining>> GetHallSoloTrainings(int hallId)
        {
            return UnitOfWork.SoloTrainingRepository.GetByFilterAsync(a => a.HallId == hallId);
        }

        public Task<IEnumerable<GroupTraining>> GetHallGroupTrainings(int hallId)
        {
            return UnitOfWork.GroupTrainingRepository.GetByFilterAsync(a => a.HallId == hallId);
        }

        public async Task<IEnumerable<User>> GetHallCoachUsers(int hallId)
        {
            var coachUserHalls = await UnitOfWork.CoachUserHallRepository.GetByFilterWithIncludesAsync(a => a.HallId == hallId, false, true);
            return coachUserHalls.Select(a => a.CoachUser);
        }

        public async Task<IEnumerable<User>> GetHallManagerUsers(int hallId)
        {
            var managerUserHalls = await UnitOfWork.ManagerUserHallRepository.GetByFilterWithIncludesAsync(a => a.HallId == hallId, false, true);
            return managerUserHalls.Select(a => a.ManagerUser);
        }

        private async Task SetSchedulesToHallAsync(int hallId, IEnumerable<ISchedule> hallSchedules)
        {
            var existsHallSchedules = await UnitOfWork.HallScheduleRepository.GetByFilterAsync(a => a.HallId == hallId);

            var toSetHallSchedules = hallSchedules.Select(a => new HallSchedule(hallId, a));

            var diff = DbHelper.EntitiesDiff(existsHallSchedules, toSetHallSchedules, new HallScheduleFullComparer(), new HallScheduleIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.HallScheduleRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.HallScheduleRepository.AddRange(diff.ToAddEntities);
            }

            if (diff.HasToUpdateEntities)
            {
                UnitOfWork.HallScheduleRepository.UpdateRange(diff.ToUpdateEntities);
            }
        }

        private async Task SetManagersToHallAsync(int hallId, IEnumerable<User> managerUsers)
        {
            var existsManagerUsers = await UnitOfWork.ManagerUserHallRepository.GetByFilterAsync(a => a.HallId == hallId);
            var neededManagerUsers = managerUsers.Select(u => new ManagerUserHall(hallId, u.Id));

            var diff = DbHelper.EntitiesDiff(existsManagerUsers, neededManagerUsers, new ManagerUserHallIdComparer());

            UnitOfWork.ManagerUserHallRepository.RemoveRange(diff.ToRemoveEntities);
            UnitOfWork.ManagerUserHallRepository.AddRange(diff.ToAddEntities);
        }

        private async Task SetCoachUsersToHallAsync(int hallId, IEnumerable<User> coachUsers)
        {
            var existsCoachHallList = await UnitOfWork.CoachUserHallRepository.
                GetByFilterAsync(a => a.HallId == hallId);
            var neededCoachUsers = coachUsers.Select(a => new CoachUserHall(hallId, a.Id));

            var diff = DbHelper.EntitiesDiff(
                existsCoachHallList, neededCoachUsers, new CoachUserHallIdComparer());

            if (diff.HasToRemoveEntities)
            {
                UnitOfWork.CoachUserHallRepository.RemoveRange(diff.ToRemoveEntities);
            }

            if (diff.HasToAddEntities)
            {
                UnitOfWork.CoachUserHallRepository.AddRange(diff.ToAddEntities);
            }
        }

        public Task<IEnumerable<Hall>> GetActiveHalls()
        {
            return GetHallsByFilterAsync(a => a.Active);
        }
    }
}
