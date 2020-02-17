using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Profile;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class ProfileExtensions
    {
        public static User ToModel(this ViewProfileViewModel viewProfileViewModel)
        {
            var model = new User
            {
                Id = viewProfileViewModel.Id,
                FirstName = viewProfileViewModel.FirstName,
                LastName = viewProfileViewModel.LastName,
                UserName = viewProfileViewModel.UserName,
                PhoneNumber = viewProfileViewModel.PhoneNumber,
                Email = viewProfileViewModel.Email,
            };
            return model;
        }

        public static User ToModel(this UpdateProfileViewModel updateProfileViewModel, User user)
        {
            var model = user;
            model.FirstName = updateProfileViewModel.FirstName;
            model.LastName = updateProfileViewModel.LastName;
            model.UserName = updateProfileViewModel.UserName;
            model.PhoneNumber = updateProfileViewModel.PhoneNumber;
            model.Email = updateProfileViewModel.Email;
            return model;
        }

        public static User ToModel(this CreateProfileViewModel createProfileViewModel)
        {
            var model = new User
            {
                FirstName = createProfileViewModel.FirstName,
                LastName = createProfileViewModel.LastName,
                UserName = createProfileViewModel.UserName,
                PhoneNumber = createProfileViewModel.PhoneNumber,
                Email = createProfileViewModel.Email,
            };

            return model;
        }

        public static CreateProfileViewModel ToCreateViewModel(this User user)
        {
            CreateProfileViewModel model = new CreateProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return model;
        }

        public static UpdateProfileViewModel ToUpdateViewModel(this User user)
        {
            var model = new UpdateProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            };

            return model;
        }

        public static async Task<ViewProfileViewModel> ToViewViewModelAsync(this User user, AppServices appServices)
        {
            var soloTrainings = await appServices.SoloTrainingService.GetSoloTrainingsByFilterAsync(a => a.ClientUserId == user.Id);
            var groupTrainings = await appServices.GroupTrainingService.GetGroupTrainingsByClientIdAsync(user.Id);
            var managerInfo = await appServices.ManagerService.GetManagerInfoByUserIdAsync(user.Id);
            var coachInfo = await appServices.CoachService.GetCoachInfoByUserIdAsync(user.Id);

            var model = new ViewProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                SoloTrainings = soloTrainings.ToList(),
                GroupTrainings = groupTrainings.ToList(),
                ViewManagerInfoViewModel = await managerInfo.ToViewViewModelAsync(appServices),
                ViewCoachInfoViewModel = await coachInfo.ToViewViewModelAsync(appServices),
            };

            return model;
        }
    }
}
