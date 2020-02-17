using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.Core.Identity.Managers
{
    public class ManualUserManager : UserManager<User>
    {
        public ManualUserManager(IManualUserStore store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<ManualUserManager> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public int SaveChanges()
        {
            return ((IManualUserStore)Store).SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return ((IManualUserStore)Store).SaveChangesAsync();
        }
    }
}
