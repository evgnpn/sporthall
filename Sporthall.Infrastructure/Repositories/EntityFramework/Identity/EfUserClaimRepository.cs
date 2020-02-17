using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfUserClaimRepository : EfRepository<UserClaim>, IRepository<UserClaim>, IUserClaimRepository
    {
        public EfUserClaimRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
