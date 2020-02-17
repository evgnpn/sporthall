using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Trainings.Group;
using System;
using System.Linq;
using System.Threading.Tasks;
using IAuthorizationService = Sporthall.Core.Services.IAuthorizationService;

namespace Sporthall.WebUI.Areas.Trainings.Controllers
{
    [Area("Trainings")]
    public class GroupController : Controller
    {
        private readonly IGroupTrainingService _groupTrainingService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        public GroupController(
            IGroupTrainingService groupTrainingServiceFactory,
            IAuthorizationService authorizationServiceFactory,
            IUserService userService)
        {
            _groupTrainingService = groupTrainingServiceFactory ?? throw new ArgumentNullException(nameof(groupTrainingServiceFactory));
            _authorizationService = authorizationServiceFactory ?? throw new ArgumentNullException(nameof(authorizationServiceFactory));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            var groupTrainings = await _groupTrainingService.GetAllGroupTrainingsAsync();
            return View((await groupTrainings.ToViewGroupTrainingViewModelsAsync(this.GetAppServices())).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var groupTraining = await _groupTrainingService.GetGroupTrainingByIdAsync(id.Value);

            if (groupTraining is null)
            {
                return NotFound();
            }

            return View(await groupTraining.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Subscribe(int groupTrainingId)
        {
            var currentUser = await _userService.GetUserAsync(User);

            await _groupTrainingService.AddClientUsersAsync(groupTrainingId, currentUser);

            return RedirectToAction("View", new { id = groupTrainingId });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Unsubscribe(int groupTrainingId)
        {
            var currentUser = await _userService.GetUserAsync(User);

            await _groupTrainingService.UnsetClientUsersAsync(groupTrainingId, currentUser);

            return RedirectToAction("View", new { id = groupTrainingId });
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Add()
        {
            return View(await new GroupTraining { Capacity = 5 }.ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(CreateGroupTrainingViewModel))]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Add(CreateGroupTrainingViewModel model)
        {
            var groupTraining = model.ToModel();
            var clientUsers = model.ClientUserSelects.GetSelectedModels();
            var coachUsers = model.CoachUserSelects.GetSelectedModels();

            await _groupTrainingService.CreateGroupTrainingAsync(groupTraining, coachUsers, clientUsers);

            return RedirectToAction("Edit", new { id = groupTraining.Id });
        }

      

        [HttpGet, ActionName("Delete"), Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var groupTraining = await _groupTrainingService.GetGroupTrainingByIdAsync(id.Value);

            if (groupTraining is null)
            {
                return NotFound();
            }

            return View(await groupTraining.ToViewViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(ViewGroupTrainingViewModel))]
        [HttpPost, ActionName("Delete"), Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(ViewGroupTrainingViewModel model)
        {
            await _groupTrainingService.RemoveGroupTrainingAsync(model.Id);

            return Ok("Групповая тренировка [id:" + model.Id + "] успешно удалена!");
        }
    }
}
