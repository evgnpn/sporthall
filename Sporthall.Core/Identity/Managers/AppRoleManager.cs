using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.Core.Identity.Managers
{
    public class AppRoleManager : RoleManager<Role>
    {
        public AppRoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) :
        base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
