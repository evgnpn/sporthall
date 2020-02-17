using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Identity.Managers;
using System.Threading.Tasks;

namespace Sporthall.Infrastructure.Identity.EntityFramework.Stores
{
    public class EfManualRoleStore : RoleStore<Role, EfDbContext, string, UserRole, RoleClaim>, IManualRoleStore
    {
        public EfManualRoleStore(EfDbContext efDbContext, IdentityErrorDescriber entityErrorDescriptor = null)
            : base(efDbContext, entityErrorDescriptor)
        {
            AutoSaveChanges = false;

        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
