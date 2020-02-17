using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface ICoachService
    {
        Task SetCoachAsync(User user, CoachInfo coachInfo);

        Task SetCoachAsync(User user, CoachInfo coachInfo, IEnumerable<Hall> halls);

        Task UpdateCoachAsync(User user, CoachInfo coachInfo);

        Task UpdateCoachAsync(User user, CoachInfo coachInfo, IEnumerable<Hall> halls);

        Task UnsetCoachAsync(User user);

        Task<CoachInfo> GetCoachInfoByUserIdAsync(string coachUserId);

        Task<IEnumerable<Hall>> GetCoachHallsAsync(string coachUserId);

        Task<IEnumerable<SoloTraining>> GetCoachSoloTrainingsAsync(string coachUserId);

        Task<IEnumerable<GroupTraining>> GetCoachGroupTrainingsAsync(string coachUserId);

        Task<User> GetCoachUserAsync(string coachUserId);

        Task<IEnumerable<User>> GetAllCoachUsersAsync();
    }
}
