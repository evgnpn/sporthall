using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfGroupTrainingRepository : EfRepository<GroupTraining>, IRepository<GroupTraining>, IGroupTrainingRepository
    {
        public EfGroupTrainingRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
