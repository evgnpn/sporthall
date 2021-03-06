﻿using Sporthall.Core.Entities;
using Sporthall.Core.Repositories;
using Sporthall.Core.Repositories.Base;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;

namespace Sporthall.Infrastructure.Repositories.EntityFramework
{
    public class EfManagerInfoRepository : EfRepository<ManagerInfo>, IRepository<ManagerInfo>, IManagerInfoRepository
    {
        public EfManagerInfoRepository(EfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
