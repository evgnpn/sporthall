using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfCoachInfoRepository : EfRepository<CoachInfo>, IRepository<CoachInfo>, ICoachInfoRepository
    {
        public EfCoachInfoRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
