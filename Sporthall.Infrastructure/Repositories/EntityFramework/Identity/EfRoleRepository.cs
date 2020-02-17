using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfRoleRepository : EfRepository<Role>, IRepository<Role>, IRoleRepository
    {
        public EfRoleRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
