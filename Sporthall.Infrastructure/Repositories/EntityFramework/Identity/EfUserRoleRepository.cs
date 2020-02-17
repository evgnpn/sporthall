using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfUserRoleRepository : EfRepository<UserRole>, IRepository<UserRole>, IUserRoleRepository
    {
        public EfUserRoleRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
