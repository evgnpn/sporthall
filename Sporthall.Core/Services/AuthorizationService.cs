using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using Sporthall.Core.Exceptions.ServiceExceptions;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class AuthorizationService : SimpleServiceBase, IAuthorizationService
    {
        public AuthorizationService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
         : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task SignInByUsernameAndPasswordAsync(string userName, string password, bool remember)
        {
            var signInRes = await SignInManager.PasswordSignInAsync(userName, password, remember, false);

            if (!signInRes.Succeeded)
            {
                if (signInRes.IsLockedOut)
                {
                    var user = await UserManager.FindByNameAsync(userName);
                    throw new ServiceException(new ServiceError("", "You are locked out until " + user.LockoutEnd, ServiceErrorType.Identity));
                }
                if (signInRes.RequiresTwoFactor)
                {
                    throw new ServiceException(new ServiceError("", "Sign-in requires two factor authorization", ServiceErrorType.Identity));
                }
                if (signInRes.IsNotAllowed)
                {
                    throw new ServiceException(new ServiceError("", "Sign-in is not allowed", ServiceErrorType.Identity));
                }
            }
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }
    }
}
