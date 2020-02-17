using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfSoloTrainingRepository : EfRepository<SoloTraining>, IRepository<SoloTraining>, ISoloTrainingRepository
    {
        public EfSoloTrainingRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
