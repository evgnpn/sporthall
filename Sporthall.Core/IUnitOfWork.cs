using Sporthall.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Sporthall.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IHallRepository HallRepository { get; }
        IHallScheduleRepository HallScheduleRepository { get; }
        ISoloTrainingRepository SoloTrainingRepository { get; }
        IGroupTrainingRepository GroupTrainingRepository { get; }
        ICoachUserHallRepository CoachUserHallRepository { get; }
        IManagerUserHallRepository ManagerUserHallRepository { get; }
        IClientUserGroupTrainingRepository ClientUserGroupTrainingRepository { get; }
        ICoachUserSoloTrainingRepository CoachUserSoloTrainingRepository { get; }
        ICoachUserGroupTrainingRepository CoachUserGroupTrainingRepository { get; }
        ICoachInfoRepository CoachInfoRepository { get; }
        IManagerInfoRepository ManagerInfoRepository { get; }

        //Identity
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserClaimRepository UserClaimRepository { get; }
        IRoleClaimRepository RoleClaimRepository { get; }
        IUserLoginRepository UserLoginRepository { get; }
        IUserTokenRepository UserTokenRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();

        void Transaction(Action action);
        Task TransactionAsync(Func<Task> action);
    }
}
