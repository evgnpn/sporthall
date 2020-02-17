using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfHallRepository : EfRepository<Hall>, IRepository<Hall>, IHallRepository
    {
        public EfHallRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
