using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Services;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using IAuthorizationService = Sporthall.Core.Services.IAuthorizationService;

namespace Sporthall.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHallService _hallService;

        public HomeController(IAuthorizationService authorizationService, IHallService hallService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _hallService = hallService ?? throw new ArgumentNullException(nameof(hallService));
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexHomeViewModel
            {
                Halls = (await _hallService.GetAllHallsAsync()).ToList()
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            return PartialView();
        }

        [ModelStateReturner(typeof(LoginViewModel))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            await _authorizationService.SignInByUsernameAndPasswordAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.Remember);

            return RedirectToAction("Index", "MyProfile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            return PartialView();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (User.Identity.IsAuthenticated)
        //        return Forbid();

        //    if (ModelState.IsValid)
        //    {
        //        var user = new User
        //        {
        //            UserName = registerViewModel.UserName,
        //            FirstName = registerViewModel.FirstName,
        //            LastName = registerViewModel.LastName,
        //            Email = registerViewModel.Email
        //        };

        //        var createUserResult =
        //            await _userManager.CreateAsync(user, registerViewModel.Password);

        //        if (createUserResult.Succeeded)
        //        {
        //            if (registerViewModel.SignInAfterRegister)
        //            {
        //                return await Login(new LoginViewModel
        //                {
        //                    UserName = registerViewModel.UserName,
        //                    Password = registerViewModel.Password,
        //                    Remember = true
        //                });
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", "ыыы");
        //    }
        //    return PartialView(registerViewModel);
        //}
    }
}
