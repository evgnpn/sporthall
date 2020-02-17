using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;

using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Trainings.Solo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Areas.Trainings.Controllers
{
    [Area("Trainings")]
    public class SoloController : Controller
    {
        private readonly ISoloTrainingService _soloTrainingService;
        private readonly IUserService _userService;

        public SoloController(ISoloTrainingService soloTrainingService, IUserService userService)
        {
            _soloTrainingService = soloTrainingService ?? throw new ArgumentNullException(nameof(soloTrainingService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var soloTrainings = await _soloTrainingService.GetAllSoloTrainingsAsync();
            return View((await soloTrainings.ToViewViewModelsAsync(this.GetAppServices())).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var soloTraining = await _soloTrainingService.GetSoloTrainingByIdAsync(id.Value);

            if (soloTraining is null)
            {
                return NotFound();
            }

            return View(await soloTraining.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Subscribe(int soloTrainingId)
        {
            var currUser = await _userService.GetUserAsync(User);

            await _soloTrainingService.SetClientUserAsync(soloTrainingId, currUser.Id);

            return RedirectToAction("View", new { id = soloTrainingId });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Unsubscribe(int soloTrainingId)
        {
            var currUser = await _userService.GetUserAsync(User);
            var soloTraining = await _soloTrainingService.GetSoloTrainingByIdAsync(soloTrainingId);

            if (soloTraining.ClientUserId != currUser.Id)
            {
                return Content("Нельзя отписать не себя");
            }

            await _soloTrainingService.UnsetClientUserAsync(soloTrainingId);

            return RedirectToAction("View", new { id = soloTrainingId });
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Add()
        {
            return View(await new SoloTraining().ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(CreateSoloTrainingViewModel))]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Add(CreateSoloTrainingViewModel model)
        {

            var soloTraining = model.ToModel();
            var coachUsers = model.CoachUserSelects?.GetSelectedModels();

            await _soloTrainingService.CreateSoloTrainingAsync(soloTraining, coachUsers);

            return RedirectToAction("View", new { id = soloTraining.Id });
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var soloTraining = await _soloTrainingService.GetSoloTrainingByIdAsync(id.Value);

            if (soloTraining is null)
            {
                return NotFound();
            }

            return View(await soloTraining.ToUpdateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(CreateSoloTrainingViewModel))]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Edit(UpdateSoloTrainingViewModel model)
        {

            var soloTraining = model.ToModel();
            var coachUsers = model.CoachUserSelects.GetSelectedModels();

            await _soloTrainingService.UpdateSoloTrainingAsync(soloTraining, coachUsers);

            return RedirectToAction("View", new { id = soloTraining.Id });
        }

        [HttpGet, ActionName("Delete"), Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var soloTraining = await _soloTrainingService.GetSoloTrainingByIdAsync(id.Value);

            if (soloTraining is null)
            {
                return NotFound();
            }

            return View(await soloTraining.ToViewViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(ViewSoloTrainingViewModel))]
        [HttpPost, ActionName("Delete"), Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(ViewSoloTrainingViewModel model)
        {
            await _soloTrainingService.RemoveSoloTrainingAsync(model.Id);

            return Ok("Индивидуальная тренировка [id:" + model.Id + "] успешно удалена!");
        }
    }
}
