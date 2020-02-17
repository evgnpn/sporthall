using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Workers.CoachUsers;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class CoachInfoExtensions
    {
        public static CoachInfo ToModel(this ViewCoachInfoViewModel viewCoachViewModel)
        {
            var model = new CoachInfo
            {
                Speciality = viewCoachViewModel.Speciality,
                Description = viewCoachViewModel.Description,
                UserId = viewCoachViewModel.CoachUser?.Id
            };
            return model;
        }

        public static CoachInfo ToModel(this UpdateCoachInfoViewModel updateCoachInfoViewModel)
        {
            var model = new CoachInfo
            {
                Speciality = updateCoachInfoViewModel.Speciality,
                Description = updateCoachInfoViewModel.Description,
                UserId = updateCoachInfoViewModel.SelectedUser?.Id,
            };

            return model;
        }

        public static CoachInfo ToModel(this CreateCoachInfoViewModel createCoachInfoViewModel)
        {
            var model = new CoachInfo
            {
                Speciality = createCoachInfoViewModel.Speciality,
                Description = createCoachInfoViewModel.Description,
                UserId = createCoachInfoViewModel.SelectedUser?.Id,
            };

            return model;
        }

        public static async Task<CreateCoachInfoViewModel> ToCreateViewModelAsync(this CoachInfo coachInfo, AppServices appServices)
        {
            if (coachInfo is null)
            {
                return null;
            }

            var model = new CreateCoachInfoViewModel
            {
                Description = coachInfo.Description,
                Speciality = coachInfo.Speciality,
                UsersForSelect = (await appServices.UserService.GetAllUsersAsync()).ToList(),
                HallSelects = (await appServices.HallService.GetAllHallsAsync()).AsSelectItems().ToList(),
                SelectedUser = null,
            };

            return model;
        }

        public static async Task<UpdateCoachInfoViewModel> ToUpdateViewModelAsync(this CoachInfo coachInfo, AppServices appServices)
        {
            if (coachInfo is null)
            {
                return null;
            }

            var coachHalls = await appServices.CoachService.GetCoachHallsAsync(coachInfo.UserId);

            var model = new UpdateCoachInfoViewModel
            {
                Description = coachInfo.Description,
                Speciality = coachInfo.Speciality,
                HallSelects = (await appServices.HallService.GetAllHallsAsync()).AsSelectItems(coachHalls, new HallIdComparer()).ToList(),
                UsersForSelect = (await appServices.UserService.GetAllUsersAsync()).ToList(),
                SelectedUser = await appServices.UserService.GetUserByIdAsync(coachInfo.UserId),
            };

            return model;
        }

        public static async Task<ViewCoachInfoViewModel> ToViewViewModelAsync(this CoachInfo coachInfo, AppServices appServices)
        {
            if (coachInfo is null)
            {
                return null;
            }

            var model = new ViewCoachInfoViewModel
            {
                Description = coachInfo.Description,
                Speciality = coachInfo.Speciality,
                CoachUser = await appServices.UserService.GetUserByIdAsync(coachInfo.UserId),
                Halls = (await appServices.CoachService.GetCoachHallsAsync(coachInfo.UserId)).ToList(),
                SoloTrainings = (await appServices.CoachService.GetCoachSoloTrainingsAsync(coachInfo.UserId)).ToList(),
                GroupTrainings = (await appServices.CoachService.GetCoachGroupTrainingsAsync(coachInfo.UserId)).ToList(),
            };

            return model;
        }
    }
}
