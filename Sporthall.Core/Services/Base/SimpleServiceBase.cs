using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using Sporthall.Core.Exceptions.ServiceExceptions;
using Sporthall.Core.Identity.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.Core.Services.Base
{
    public abstract class SimpleServiceBase
    {
        protected AppUserManager UserManager { get; private set; }
        protected SignInManager<User> SignInManager { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }

        public SimpleServiceBase(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            SignInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected async Task ThrowIfUserNotInRoleAsync(User user, string role)
        {
            if (user is null)
            {
                return;
            }

            if (!await UserManager.IsInRoleAsync(user, role))
            {
                throw new ServiceException(new ServiceError("", $"Given user ('${user.UserName}') not in role '${role}'", ServiceErrorType.Identity));
            }
        }

        protected async Task ThrowIfUsersNotInRoleAsync(IEnumerable<User> users, string role)
        {
            if (users is null || users.Count() < 1)
            {
                return;
            }

            var usersNotInRole = await UserManager.GetUsersNotInRoleAsync(users, role);

            if (usersNotInRole.Count() > 0)
            {
                string userNames = usersNotInRole.Select(a => a.UserName).Aggregate((a, b) => a + ", " + b);
                throw new ServiceException(new ServiceError("", $"Given user/s ('${userNames}') not in role '${role}'", ServiceErrorType.Identity));
            }
        }

        protected async Task ThrowIfUserInRoleAsync(User user, string role)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await UserManager.IsInRoleAsync(user, role))
            {
                throw new ServiceException(new ServiceError("", $"Given user ('${user.UserName}') already has '${role}' role", ServiceErrorType.Identity));
            }
        }

        protected void ThrowIfIdentityNotSuccess(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(a => new ServiceError(a.Code, a.Description, ServiceErrorType.Identity))?.ToArray();
                throw new ServiceException(errors);
            }
        }
    }
}
