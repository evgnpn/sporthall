using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Trainings.Group;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class GroupTrainingExtensions
    {
        public static GroupTraining ToModel(this GroupTrainingViewModel groupTraining)
        {
            return new GroupTraining
            {
                Active = groupTraining.Active,
                Description = groupTraining.Description,
                Capacity = groupTraining.Capacity,
                Date = groupTraining.Date,
                BeginTime = groupTraining.BeginTime,
                EndTime = groupTraining.EndTime,
            };
        }

        public static GroupTraining ToModel(this ViewGroupTrainingViewModel model)
        {
            var groupTraining = ToModel((GroupTrainingViewModel)model);
            groupTraining.Id = model.Id;
            groupTraining.HallId = model.Hall.Id;
            return groupTraining;
        }

        public static GroupTraining ToModel(this UpdateGroupTrainingViewModel model)
        {
            var groupTraining = new GroupTraining();
            groupTraining.Active = groupTraining.Active;
            groupTraining.Description = groupTraining.Description;
            groupTraining.Capacity = groupTraining.Capacity;
            groupTraining.Date = groupTraining.Date;
            groupTraining.BeginTime = groupTraining.BeginTime;
            groupTraining.EndTime = groupTraining.EndTime;
            groupTraining.Id = model.Id;
            groupTraining.HallId = model.SelectedHall.Id;
            return groupTraining;
        }

        public static GroupTraining ToModel(this CreateGroupTrainingViewModel model)
        {
            var groupTraining = new GroupTraining();
            groupTraining.Active = groupTraining.Active;
            groupTraining.Description = groupTraining.Description;
            groupTraining.Capacity = groupTraining.Capacity;
            groupTraining.Date = groupTraining.Date;
            groupTraining.BeginTime = groupTraining.BeginTime;
            groupTraining.EndTime = groupTraining.EndTime;
            groupTraining.HallId = model.SelectedHall.Id;
            return groupTraining;
        }

        public static GroupTrainingViewModel ToViewModel(this GroupTraining groupTraining)
        {
            return new GroupTrainingViewModel(groupTraining);
        }

        public static async Task<CreateGroupTrainingViewModel> ToCreateViewModelAsync(this GroupTraining groupTraining, AppServices appServices)
        {
            return new CreateGroupTrainingViewModel(
                groupTraining: groupTraining,
                hallsForSelect: (await appServices.HallService.GetAllHallsAsync()).ToList(),
                clientUserSelects: (await appServices.UserService.GetAllUsersAsync()).AsSelectItems().ToList(),
                coachUserSelects: (await appServices.CoachService.GetAllCoachUsersAsync()).AsSelectItems().ToList()
            );
        }

        public static async Task<UpdateGroupTrainingViewModel> ToUpdateViewModelAsync(this GroupTraining groupTraining, AppServices appServices)
        {
            var choicedClientUsers = await appServices.GroupTrainingService.GetClientUsersAsync(groupTraining.Id);
            var choicedCoachUsers = await appServices.GroupTrainingService.GetCoachUsersAsync(groupTraining.Id);

            var model = new UpdateGroupTrainingViewModel
            (
                groupTraining: groupTraining,
                hallsForSelect: (await appServices.HallService.GetAllHallsAsync()).ToList(),
                selectedHall: await appServices.HallService.GetHallByIdAsync(groupTraining.HallId),
                clientUserSelects: (await appServices.UserService.GetAllUsersAsync()).AsSelectItems(choicedClientUsers, new UserIdComparer()).ToList(),
                coachUserSelects: (await appServices.CoachService.GetAllCoachUsersAsync()).AsSelectItems(choicedCoachUsers, new UserIdComparer()).ToList()
            );

            return model;
        }

        public static async Task<ViewGroupTrainingViewModel> ToViewViewModelAsync(this GroupTraining groupTraining, AppServices appServices)
        {
            var model = new ViewGroupTrainingViewModel(
              groupTraining: groupTraining,
              hall: await appServices.HallService.GetHallByIdAsync(groupTraining.HallId),
              coachUsers: (await appServices.GroupTrainingService.GetCoachUsersAsync(groupTraining.Id)).ToList(),
              clientUsers: (await appServices.GroupTrainingService.GetClientUsersAsync(groupTraining.Id)).ToList()
           );

            return model;
        }

        public static async Task<IEnumerable<ViewGroupTrainingViewModel>> ToViewGroupTrainingViewModelsAsync(this IEnumerable<GroupTraining> groupTrainings, AppServices appServices)
        {
            var list = new List<ViewGroupTrainingViewModel>();

            foreach (var groupTraining in groupTrainings)
            {
                list.Add(await groupTraining.ToViewViewModelAsync(appServices));
            }

            return list;
        }
    }
}
