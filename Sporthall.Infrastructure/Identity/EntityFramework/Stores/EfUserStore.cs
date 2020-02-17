using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Identity;

namespace Sporthall.Infrastructure.Identity.EntityFramework.Stores
{
    public class EfUserStore : UserStore<User, Role, EfDbContext, string, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public EfUserStore(EfDbContext efDbContext, IdentityErrorDescriber entityErrorDescriptor = null)
            : base(efDbContext, entityErrorDescriptor)
        {
            AutoSaveChanges = true;
        }
    }
}
