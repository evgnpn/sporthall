using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions;
using Sporthall.WebUI.Extensions.ViewModels;
using Sporthall.WebUI.Filters;
using Sporthall.WebUI.ViewModels.Halls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Controllers
{
    public class HallsController : Controller
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService ?? throw new ArgumentNullException(nameof(hallService));
        }

        public async Task<IActionResult> Index()
        {
            var halls = await _hallService.GetAllHallsAsync();
            var models = await halls.ToViewViewModelsAsync(this.GetAppServices());

            return View(models.ToList());
        }

        public async Task<IActionResult> View(int id)
        {
            var hall = await _hallService.GetHallByIdAsync(id);
            return View(await hall.ToViewViewModelAsync(this.GetAppServices()));
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public IActionResult Add()
        {
            return View(new Hall().ToCreateViewModel());
        }

        [ModelStateReturner(typeof(CreateHallViewModel))]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Add(CreateHallViewModel createHallViewModel)
        {
            var hall = createHallViewModel.ToModel();
            var hallSchedules = createHallViewModel.HallScheduleSelects.GetSelectedModels();

            await _hallService.CreateHallAsync(hall, hallSchedules);

            return RedirectToAction("Edit", new { id = hall.Id });
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var hall = await _hallService.GetHallByIdAsync(id.Value);

            if (hall is null)
            {
                return NotFound();
            }

            return View(await hall.ToUpdateViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(UpdateHallViewModel))]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Edit(UpdateHallViewModel model)
        {
            var hall = model.ToModel();
            var hallSchedules = model.HallScheduleSelects?.GetSelectedModels();

            await _hallService.UpdateHallAsync(hall, hallSchedules);

            return RedirectToAction("View", new { id = model.Id });
        }

        [HttpGet, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var hall = await _hallService.GetHallByIdAsync(id.Value);

            if (hall is null)
            {
                return NotFound();
            }

            return View(await hall.ToViewViewModelAsync(this.GetAppServices()));
        }

        [ModelStateReturner(typeof(ViewHallViewModel), ReturnOnInvalidModelState = false)]
        [HttpPost, Authorize(Roles = "Manager,GeneralManager")]
        public async Task<IActionResult> Delete(ViewHallViewModel model)
        {
            await _hallService.RemoveHallAsync(model.Id);

            return Ok("Зал успешно удален");
        }
    }
}
