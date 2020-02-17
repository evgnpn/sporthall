using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Trainings.Solo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class SoloTrainingExtensions
    {
        public static SoloTraining ToModel(this SoloTrainingViewModel soloTraining)
        {
            return new SoloTraining
            {
                Active = soloTraining.Active,
                Description = soloTraining.Description,
                Date = soloTraining.Date,
                BeginTime = soloTraining.BeginTime,
                EndTime = soloTraining.EndTime,
            };
        }

        public static SoloTraining ToModel(this EditSoloTrainingViewModel editSoloTrainingViewModel)
        {
            var soloTraining = new SoloTraining();
            soloTraining.Active = soloTraining.Active;
            soloTraining.Description = soloTraining.Description;
            soloTraining.Date = soloTraining.Date;
            soloTraining.BeginTime = soloTraining.BeginTime;
            soloTraining.EndTime = soloTraining.EndTime;
            soloTraining.HallId = editSoloTrainingViewModel.SelectedHall.Id;
            soloTraining.ClientUserId = editSoloTrainingViewModel.SelectedClientUser.Id;
            return soloTraining;
        }

        public static SoloTraining ToModel(this ViewSoloTrainingViewModel viewSoloTrainingViewModel)
        {
            var soloTraining = ToModel((SoloTrainingViewModel)viewSoloTrainingViewModel);
            soloTraining.Id = viewSoloTrainingViewModel.Id;
            soloTraining.HallId = viewSoloTrainingViewModel.Hall.Id;
            soloTraining.ClientUserId = viewSoloTrainingViewModel.ClientUser.Id;
            return soloTraining;
        }

        public static SoloTraining ToModel(this UpdateSoloTrainingViewModel updateSoloTrainingViewModel)
        {
            var soloTraining = ToModel((EditSoloTrainingViewModel)updateSoloTrainingViewModel);
            soloTraining.Id = updateSoloTrainingViewModel.Id;
            return soloTraining;
        }

        public static SoloTraining ToModel(this CreateSoloTrainingViewModel createSoloTrainingViewModel)
        {
            var soloTraining = ToModel((EditSoloTrainingViewModel)createSoloTrainingViewModel);
            return soloTraining;
        }

        public static async Task<CreateSoloTrainingViewModel> ToCreateViewModelAsync(this SoloTraining soloTraining, AppServices appServices)
        {
            var allHalls = await appServices.HallService.GetAllHallsAsync();
            var allUsers = await appServices.UserService.GetAllUsersAsync();
            var allCoachUsers = await appServices.CoachService.GetAllCoachUsersAsync();

            var model = new CreateSoloTrainingViewModel
            {
                Description = soloTraining.Description,
                BeginTime = soloTraining.BeginTime,
                Date = soloTraining.Date,
                EndTime = soloTraining.EndTime,
                HallsForSelect = allHalls.ToList(),
                ClientUsersForSelect = allUsers.ToList(),
                CoachUserSelects = allCoachUsers.AsSelectItems().ToList(),
            };

            return model;
        }

        public static async Task<UpdateSoloTrainingViewModel> ToUpdateViewModelAsync(this SoloTraining soloTraining, AppServices appServices)
        {
            var selectedCoachUsers = await appServices.SoloTrainingService.GetCoachUsersAsync(soloTraining.Id);
            var selectedHall = await appServices.SoloTrainingService.GetSoloTrainingHallAsync(soloTraining);
            var selectedClientUser = await appServices.SoloTrainingService.GetClientUserAsync(soloTraining.Id);

            var hallsForSelect = (await appServices.HallService.GetAllHallsAsync())?.ToList();
            var clientUsersForSelect = (await appServices.UserService.GetAllUsersAsync())?.ToList();
            var coachUserSelects = (await appServices.CoachService.GetAllCoachUsersAsync())?.AsSelectItems(selectedCoachUsers, new UserIdComparer())?.ToList();

            return new UpdateSoloTrainingViewModel(soloTraining, selectedHall, selectedClientUser, hallsForSelect, clientUsersForSelect, coachUserSelects);
        }

        public static async Task<ViewSoloTrainingViewModel> ToViewViewModelAsync(this SoloTraining soloTraining, AppServices appServices)
        {
            var coachUsers = (await appServices.SoloTrainingService.GetCoachUsersAsync(soloTraining.Id))?.ToList();
            var clientUser = await appServices.SoloTrainingService.GetClientUserAsync(soloTraining.Id);
            var hall = await appServices.SoloTrainingService.GetSoloTrainingHallAsync(soloTraining);

            return new ViewSoloTrainingViewModel(soloTraining, hall, clientUser, coachUsers);
        }

        public static async Task<IEnumerable<ViewSoloTrainingViewModel>> ToViewViewModelsAsync(this IEnumerable<SoloTraining> soloTrainings, AppServices appServices)
        {
            var list = new List<ViewSoloTrainingViewModel>();

            foreach (var soloTraining in soloTrainings)
            {
                list.Add(await soloTraining.ToViewViewModelAsync(appServices));
            }

            return list;
        }
    }
}
