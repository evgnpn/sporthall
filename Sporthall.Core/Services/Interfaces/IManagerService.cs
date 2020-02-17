using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IManagerService
    {
        Task SetManagerAsync(ManagerInfo managerInfo);

        Task SetManagerAsync(ManagerInfo managerInfo, IEnumerable<Hall> halls);

        Task UpdateManagerAsync(ManagerInfo managerInfo);

        Task UpdateManagerAsync(ManagerInfo managerInfo, IEnumerable<Hall> halls);

        Task UnsetManagerAsync(User user);

        Task<ManagerInfo> GetManagerInfoByUserIdAsync(string managerUserId);

        Task<IEnumerable<Hall>> GetManagerHallsAsync(string managerUserId);

        Task<User> GetManagerUserAsync(string managerUserId);

        Task<IEnumerable<User>> GetAllManagerUsersAsync();
    }
}
