using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.Core.Identity.Managers
{
    public class ManualRoleManager : RoleManager<Role>
    {
        public ManualRoleManager(IManualRoleStore store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<AppRoleManager> logger) :
        base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public int SaveChanges()
        {
            return ((IManualRoleStore)Store).SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return ((IManualRoleStore)Store).SaveChangesAsync();
        }
    }
}
