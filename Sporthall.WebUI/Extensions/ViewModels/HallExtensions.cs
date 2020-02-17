using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Halls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class HallExtensions
    {
        public static Hall ToModel(this ViewHallViewModel viewHallViewModel)
        {
            var model = new Hall
            {
                Id = viewHallViewModel.Id,
                Description = viewHallViewModel.Description,
                Address = viewHallViewModel.Address,
                Name = viewHallViewModel.Name,
                PhoneNumber = viewHallViewModel.Name,
            };

            return model;
        }

        public static Hall ToModel(this UpdateHallViewModel updateHallViewModel)
        {
            var model = new Hall
            {
                Id = updateHallViewModel.Id,
                Description = updateHallViewModel.Description,
                Address = updateHallViewModel.Address,
                Name = updateHallViewModel.Name,
                PhoneNumber = updateHallViewModel.Name,
            };

            return model;
        }

        public static Hall ToModel(this CreateHallViewModel createHallViewModel)
        {
            var model = new Hall
            {
                Description = createHallViewModel.Description,
                Address = createHallViewModel.Address,
                Name = createHallViewModel.Name,
                PhoneNumber = createHallViewModel.Name,
            };

            return model;
        }

        public static CreateHallViewModel ToCreateViewModel(this Hall hall)
        {
            return new CreateHallViewModel(hall, ScheduleExtensions.GetAllScheduleDays().AsAllScheduleSelectors().ToList());
        }

        public static async Task<UpdateHallViewModel> ToUpdateViewModelAsync(this Hall hall, AppServices appServices)
        {
            return new UpdateHallViewModel(hall, (await appServices.HallService.GetHallSchedulesAsync(hall.Id)).AsAllScheduleSelectors().ToList());
        }

        public static async Task<ViewHallViewModel> ToViewViewModelAsync(this Hall hall, AppServices appServices)
        {
            var hallScheduleSelects = (await appServices.HallService.GetHallSchedulesAsync(hall.Id))?.AsAllScheduleSelectors()?.ToList();
            var hallSoloTrainings = (await appServices.HallService.GetHallSoloTrainings(hall.Id)).ToList();
            var hallGroupTrainings = (await appServices.HallService.GetHallGroupTrainings(hall.Id)).ToList();
            var coachUsers = (await appServices.HallService.GetHallCoachUsers(hall.Id)).ToList();
            var managerUsers = (await appServices.HallService.GetHallManagerUsers(hall.Id)).ToList();

            return new ViewHallViewModel(hall, hallScheduleSelects, hallSoloTrainings, hallGroupTrainings, managerUsers, coachUsers);
        }

        public static async Task<IEnumerable<ViewHallViewModel>> ToViewViewModelsAsync(this IEnumerable<Hall> halls, AppServices appServices)
        {
            var list = new List<ViewHallViewModel>();

            foreach (var hall in halls)
            {
                list.Add(await hall.ToViewViewModelAsync(appServices));
            }

            return list;
        }
    }
}
