using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sporthall.Core.Entities;

namespace Sporthall.Core.Identity.Managers
{
    public class TestSignInManager : SignInManager<User>
    {
        //public TestSignInManager(UserManager<User> userManager,
        //    IHttpContextAccessor httpContextAccessor,
        //    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        //    IOptions<IdentityOptions> options,
        //    ILogger<SignInManager<User>> logger,
        //    IAuthenticationSchemeProvider authenticationSchemeProvider,
        //    IUserConfirmation<User> userConfirmation)
        //    : base(userManager, httpContextAccessor, userClaimsPrincipalFactory, options, logger, authenticationSchemeProvider, userConfirmation)
        //{
        //}
        public TestSignInManager(AppUserManager userManager,
        IHttpContextAccessor httpContextAccessor,
        AppUserClaimsPrincipalFactory userClaimsPrincipalFactory,
        IOptions<IdentityOptions> options,
        ILogger<SignInManager<User>> logger,
        IAuthenticationSchemeProvider authenticationSchemeProvider,
        IUserConfirmation<User> userConfirmation)
        : base(userManager, httpContextAccessor, userClaimsPrincipalFactory, options, logger, authenticationSchemeProvider, userConfirmation)
        {
        }
    }
}
