using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Identity;

namespace Sporthall.Infrastructure.Identity.EntityFramework.Stores
{
    public class EfRoleStore : RoleStore<Role, EfDbContext, string, UserRole, RoleClaim>
    {
        public EfRoleStore(EfDbContext efDbContext, IdentityErrorDescriber entityErrorDescriptor = null)
            : base(efDbContext, entityErrorDescriptor)
        {
            AutoSaveChanges = true;
        }
    }
}
