using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfUserLoginRepository : EfRepository<UserLogin>, IRepository<UserLogin>, IUserLoginRepository
    {
        public EfUserLoginRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
