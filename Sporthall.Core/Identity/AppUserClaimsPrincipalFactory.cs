using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sporthall.Core.Entities;
using Sporthall.Core.Identity.Managers;

namespace Sporthall.Core.Identity
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public AppUserClaimsPrincipalFactory(AppUserManager userManager, AppRoleManager roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }
    }
}
