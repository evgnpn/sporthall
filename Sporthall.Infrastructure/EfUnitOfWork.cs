using Sporthall.Core;
using Sporthall.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Sporthall.Infrastructure.Repositories.EntityFramework.Base
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EfDbContext _dbContext;

        public IHallRepository HallRepository { get; }
        public IHallScheduleRepository HallScheduleRepository { get; }
        public ISoloTrainingRepository SoloTrainingRepository { get; }
        public IGroupTrainingRepository GroupTrainingRepository { get; }
        public ICoachUserHallRepository CoachUserHallRepository { get; }
        public IManagerUserHallRepository ManagerUserHallRepository { get; }
        public IClientUserGroupTrainingRepository ClientUserGroupTrainingRepository { get; }
        public ICoachUserSoloTrainingRepository CoachUserSoloTrainingRepository { get; }
        public ICoachUserGroupTrainingRepository CoachUserGroupTrainingRepository { get; }
        public ICoachInfoRepository CoachInfoRepository { get; }
        public IManagerInfoRepository ManagerInfoRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }

        public IUserClaimRepository UserClaimRepository { get; }

        public IRoleClaimRepository RoleClaimRepository { get; }

        public IUserLoginRepository UserLoginRepository { get; }

        public IUserTokenRepository UserTokenRepository { get; }

        public IUserRoleRepository UserRoleRepository { get; }

        public EfUnitOfWork(EfDbContext dbContext)
        {
            HallRepository = new EfHallRepository(dbContext);
            HallScheduleRepository = new EfHallScheduleRepository(dbContext);
            SoloTrainingRepository = new EfSoloTrainingRepository(dbContext);
            GroupTrainingRepository = new EfGroupTrainingRepository(dbContext);
            CoachUserHallRepository = new EfCoachUserHallRepository(dbContext);
            ManagerUserHallRepository = new EfManagerUserHallRepository(dbContext);
            ClientUserGroupTrainingRepository = new EfClientUserGroupTrainingRepository(dbContext);
            CoachUserSoloTrainingRepository = new EfCoachUserSoloTrainingRepository(dbContext);
            CoachUserGroupTrainingRepository = new EfCoachUserGroupTrainingRepository(dbContext);
            CoachInfoRepository = new EfCoachInfoRepository(dbContext);
            ManagerInfoRepository = new EfManagerInfoRepository(dbContext);

            UserRepository = new EfUserRepository(dbContext);
            RoleRepository = new EfRoleRepository(dbContext);
            UserClaimRepository = new EfUserClaimRepository(dbContext);
            RoleClaimRepository = new EfRoleClaimRepository(dbContext);
            UserLoginRepository = new EfUserLoginRepository(dbContext);
            UserTokenRepository = new EfUserTokenRepository(dbContext);
            UserRoleRepository = new EfUserRoleRepository(dbContext);

            _dbContext = dbContext;
        }

        public void Transaction(Action inTransactionAction)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    inTransactionAction.Invoke();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task TransactionAsync(Func<Task> inTransactionAction)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await inTransactionAction.Invoke();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
