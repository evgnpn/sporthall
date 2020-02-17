using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Controllers
{
    [Authorize(Roles = "Manager,GeneralManager")]
    public class ProfilesController : Controller
    {
        private readonly IUserService _userService;

        public ProfilesController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Index(string role, string search)
        {
            List<User> users = new List<User>();

            if (string.IsNullOrEmpty(role))
            {
                users.AddRange(await _userService.GetAllUsersAsync());
            }
            //else if (role == AppRoles.Coach)
            //{
            //    users.AddRange(await _userService.get());
            //}

            return View(users);
        }

        [HttpGet("[controller]/[action]/{username}"), AllowAnonymous]
        public new async Task<IActionResult> View(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var user = await _userService.GetUserByUserName(username);

            if (user is null)
            {
                return NotFound();
            }

            return View(await user.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpGet("[controller]/[action]/{username}")]
        public async Task<IActionResult> Edit(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var user = await _userService.GetUserByUserName(username);

            if (user is null)
            {
                return NotFound();
            }

            return View(user.ToUpdateViewModel());
        }

        [ModelStateReturner(typeof(UpdateProfileViewModel))]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileViewModel model)
        {
            var user = await _userService.GetUserByIdAsync(model.Id);
            var userToUpdate = model.ToModel(user);

            await _userService.UpdateUserAsync(userToUpdate);

            return RedirectToAction("View", new { id = user.Id });
        }
    }
}
