using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfHallScheduleRepository : EfRepository<HallSchedule>, IRepository<HallSchedule>, IHallScheduleRepository
    {
        public EfHallScheduleRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
