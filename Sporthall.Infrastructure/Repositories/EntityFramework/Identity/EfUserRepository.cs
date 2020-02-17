using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfUserRepository : EfRepository<User>, IRepository<User>, IUserRepository
    {
        public EfUserRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
