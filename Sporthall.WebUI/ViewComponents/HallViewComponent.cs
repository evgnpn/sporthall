using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.Extensions.ViewModels;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewComponents
{
    [ViewComponent(Name = "HallOverview")]
    public class HallOverviewComponent : ViewComponent
    {
        private readonly AppServices _appServices;

        public HallOverviewComponent(AppServices appServices)
        {
            _appServices = appServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Hall hall)
        {
            var model = await hall.ToViewViewModelAsync(_appServices);
            return View(model);
        }
    }
}
