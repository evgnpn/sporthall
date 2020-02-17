using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IHallService
    {
        Task CreateHallAsync(Hall hall);

        Task CreateHallAsync(Hall hall,
            IEnumerable<ISchedule> hallSchedules = null,
            IEnumerable<User> managerUsers = null,
            IEnumerable<User> coachUsers = null);

        Task UpdateHallAsync(Hall hall);

        Task UpdateHallAsync(Hall hall,
            IEnumerable<ISchedule> hallSchedules = null,
            IEnumerable<User> managerUsers = null,
            IEnumerable<User> coachUsers = null);

        Task RemoveHallAsync(int id);

        Task<Hall> GetHallByIdAsync(int soloTrainingId);

        Task<IEnumerable<Hall>> GetAllHallsAsync();

        Task<IEnumerable<Hall>> GetHallsByFilterAsync(Func<Hall, bool> predicate);

        Task<IEnumerable<Hall>> GetActiveHalls();

        Task<IEnumerable<HallSchedule>> GetHallSchedulesAsync(int hallId);

        Task<IEnumerable<SoloTraining>> GetHallSoloTrainings(int hallId);

        Task<IEnumerable<GroupTraining>> GetHallGroupTrainings(int hallId);

        Task<IEnumerable<User>> GetHallCoachUsers(int hallId);

        Task<IEnumerable<User>> GetHallManagerUsers(int hallId);
    }
}
