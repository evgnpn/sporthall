using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface ISoloTrainingService
    {
        Task CreateSoloTrainingAsync(SoloTraining soloTraining);

        Task CreateSoloTrainingAsync(SoloTraining soloTraining, IEnumerable<User> coachUsers = null);

        Task UpdateSoloTrainingAsync(SoloTraining soloTraining);

        Task UpdateSoloTrainingAsync(SoloTraining soloTraining, IEnumerable<User> coachUsers = null);

        Task RemoveSoloTrainingAsync(int id);

        Task SetClientUserAsync(int soloTrainingId, string userId);

        Task UnsetClientUserAsync(int soloTrainingId);

        Task<User> GetClientUserAsync(int soloTrainingId);

        Task SetCoachUsersAsync(int soloTrainingId, params User[] coachUsers);

        Task<IEnumerable<User>> GetCoachUsersAsync(int soloTrainingId);

        Task UnsetCoachUsersAsync(int soloTrainingId, params User[] coachUsers);

        Task<SoloTraining> GetSoloTrainingByIdAsync(int soloTrainingId);

        Task<IEnumerable<SoloTraining>> GetAllSoloTrainingsAsync();

        Task<IEnumerable<SoloTraining>> GetSoloTrainingsByFilterAsync(Expression<Func<SoloTraining, bool>> predicate);

        Task<IEnumerable<SoloTraining>> GetActiveSoloTrainings();

        Task<Hall> GetSoloTrainingHallAsync(SoloTraining soloTraining);

        Task<IEnumerable<SoloTraining>> GetUserSoloTrainings(string userId);
    }
}
