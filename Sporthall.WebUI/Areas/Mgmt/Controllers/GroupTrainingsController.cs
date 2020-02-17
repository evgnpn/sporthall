using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Trainings.Group;

namespace Sporthall.WebUI.Areas.Mgmt.Controllers
{
    [Area("Mgmt"), Authorize(Roles="Manager")]
    public class GroupTrainingsController : Controller
    {

        private readonly IGroupTrainingService _groupTrainingService;

        public GroupTrainingsController(IGroupTrainingService groupTrainingService)
        {
            _groupTrainingService = groupTrainingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View((await _groupTrainingService.GetAllGroupTrainingsAsync()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(await new GroupTraining { Capacity = 5 }.ToCreateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(CreateGroupTrainingViewModel))]
        [HttpPost]
        public async Task<IActionResult> Add(CreateGroupTrainingViewModel model)
        {
            var groupTraining = model.ToModel();
            var clientUsers = model.ClientUserSelects.GetSelectedModels();
            var coachUsers = model.CoachUserSelects.GetSelectedModels();

            await _groupTrainingService.CreateGroupTrainingAsync(groupTraining, coachUsers, clientUsers);

            return RedirectToAction("Edit", new { id = groupTraining.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var groupTraining = await _groupTrainingService.GetGroupTrainingByIdAsync(id.Value);

            if (groupTraining is null)
            {
                return NotFound();
            }

            return View(await groupTraining.ToUpdateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(UpdateGroupTrainingViewModel))]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateGroupTrainingViewModel model)
        {
            var groupTraining = model.ToModel();
            var clientUsers = model.ClientUserSelects.GetSelectedModels();
            var coachUsers = model.CoachUserSelects.GetSelectedModels();

            await _groupTrainingService.UpdateGroupTrainingAsync(groupTraining, coachUsers, clientUsers);

            return RedirectToAction("View", new { id = groupTraining.Id });
        }

    }
}
