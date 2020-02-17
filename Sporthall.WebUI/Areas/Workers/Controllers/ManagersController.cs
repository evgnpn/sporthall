using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Workers.Managers;
using System;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Areas.Profiles.Controllers
{
    [Area("Workers")]
    [Authorize(Roles = "Manager,GeneralManager")]
    public class ManagersController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService ?? throw new ArgumentNullException(nameof(managerService));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var managers = await _managerService.GetAllManagerUsersAsync();
            return View(managers);
        }

        [HttpGet]
        [AllowAnonymous]
        public new async Task<IActionResult> View(string id)
        {
            var manager = await _managerService.GetManagerInfoByUserIdAsync(id);
            return View(await manager.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(await new ManagerInfo().ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(UpdateManagerInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Add(UpdateManagerInfoViewModel updateManagerInfoViewModel)
        {
            var user = updateManagerInfoViewModel.SelectedManagerUser;
            var managerInfo = updateManagerInfoViewModel.ToModel();
            var halls = updateManagerInfoViewModel.HallSelects.GetSelectedModels();

            await _managerService.SetManagerAsync(managerInfo, halls);

            return RedirectToAction("View", new { id = managerInfo.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ManagerInfo manager = await _managerService.GetManagerInfoByUserIdAsync(id);
            return View(await manager.ToUpdateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(UpdateManagerInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateManagerInfoViewModel updateViewModel)
        {
            var managerInfo = updateViewModel.ToModel();
            var halls = updateViewModel.HallSelects.GetSelectedModels();

            await _managerService.UpdateManagerAsync(managerInfo, halls);

            return RedirectToAction("View", new { id = managerInfo.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var manager = await _managerService.GetManagerInfoByUserIdAsync(id);
            return View("Delete", await manager.ToViewViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(ViewManagerInfoViewModel))]
        [HttpPost]
        public async Task<IActionResult> Delete(ViewManagerInfoViewModel model)
        {
            await _managerService.UnsetManagerAsync(model.ManagerUser);
            return Ok("Тренер успешно удален!");
        }
    }
}
