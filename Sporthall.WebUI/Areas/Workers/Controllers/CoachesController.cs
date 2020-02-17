using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Workers.CoachUsers;
using System;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Areas.Profiles.Controllers
{
    [Area("Workers")]
    [Authorize(Roles = "Manager,GeneralManager")]
    public class CoachUsersController : Controller
    {
        private readonly ICoachService _coachService;

        public CoachUsersController(ICoachService coachService)
        {
            _coachService = coachService ?? throw new ArgumentNullException(nameof(coachService));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var coachUsers = await _coachService.GetAllCoachUsersAsync();
            return View(coachUsers);
        }

        [HttpGet]
        [AllowAnonymous]
        public new async Task<IActionResult> View(string id)
        {
            var coachInfo = await _coachService.GetCoachUserAsync(id);
            return View(await coachInfo.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(await new CoachInfo().ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(CreateCoachInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Add(CreateCoachInfoViewModel createCoachInfoViewModel)
        {
            var user = createCoachInfoViewModel.SelectedUser;
            var coachInfo = createCoachInfoViewModel.ToModel();
            var halls = createCoachInfoViewModel.HallSelects.GetSelectedModels();

            await _coachService.SetCoachAsync(user, coachInfo, halls);

            return RedirectToAction("View", new { id = coachInfo.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var coachInfo = await _coachService.GetCoachInfoByUserIdAsync(id);
            return View(await coachInfo.ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(UpdateCoachInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCoachInfoViewModel updateCoachInfoViewModel)
        {
            var user = updateCoachInfoViewModel.SelectedUser;
            var coachInfo = updateCoachInfoViewModel.ToModel();
            var halls = updateCoachInfoViewModel.HallSelects.GetSelectedModels();

            await _coachService.UpdateCoachAsync(user, coachInfo, halls);

            return RedirectToAction("View", new { id = coachInfo.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var coachInfo = await _coachService.GetCoachInfoByUserIdAsync(id);
            return View("Delete", coachInfo.ToViewViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(ViewCoachInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Delete(ViewCoachInfoViewModel model)
        {
            await _coachService.UnsetCoachAsync(model.CoachUser);

            return Ok("Тренер успешно удален!");
        }
    }
}
