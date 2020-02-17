using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfUserTokenRepository : EfRepository<UserToken>, IRepository<UserToken>, IUserTokenRepository
    {
        public EfUserTokenRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
