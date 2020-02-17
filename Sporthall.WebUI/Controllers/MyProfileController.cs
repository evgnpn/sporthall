using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Profile;
using System;
using System.Threading.Tasks;
using IAuthorizationService = Sporthall.Core.Services.IAuthorizationService;

namespace Sporthall.WebUI.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public MyProfileController(
            IUserService userService,
            IAuthorizationService authorizationService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserAsync(User);
            return View(await user.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userService.GetUserAsync(User);
            return View(user.ToUpdateViewModel());
        }

        [ModelStateReturner(typeof(UpdateProfileViewModel))]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileViewModel model)
        {
            var currUser = await _userService.GetUserAsync(User);
            var userToUpdate = model.ToModel(currUser);

            await _userService.UpdateUserAsync(userToUpdate);

            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(UpdatePasswordViewModel changePasswordViewModel)
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //    if (ModelState.IsValid)
        //    {
        //        var passResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var passResetRes =
        //            await _userManager.ResetPasswordAsync(user, passResetToken, changePasswordViewModel.Password);
        //        if (!passResetRes.Succeeded)
        //        {
        //            foreach (var err in passResetRes.Errors)
        //                ModelState.AddModelError("pass", err.Description);
        //        }
        //    }

        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _authorizationService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
