using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfRoleClaimRepository : EfRepository<RoleClaim>, IRepository<RoleClaim>, IRoleClaimRepository
    {
        public EfRoleClaimRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
