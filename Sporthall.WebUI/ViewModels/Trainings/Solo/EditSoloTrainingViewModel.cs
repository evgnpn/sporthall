using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewModels.Trainings.Solo
{
    public class EditSoloTrainingViewModel : IRebuildableAsync
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Active { get; set; }
        public Hall SelectedHall { get; set; }
        public User SelectedClientUser { get; set; }
        public List<Hall> HallsForSelect { get; set; }
        public List<User> ClientUsersForSelect { get; set; }
        public List<SelectItem<User>> CoachUserSelects { get; set; }
        public EditSoloTrainingViewModel()
        {
        }
        public EditSoloTrainingViewModel(SoloTraining soloTraining,
            Hall selectedHall, User selectedClientUser, List<Hall> hallsForSelect, List<User> clientUsersForSelect, List<SelectItem<User>> coachUserSelects)
        {
            if (soloTraining is null)
            {
                throw new ArgumentNullException(nameof(soloTraining));
            }
            Name = soloTraining.Name;
            Description = soloTraining.Description;
            Date = soloTraining.Date;
            BeginTime = soloTraining.BeginTime;
            EndTime = soloTraining.EndTime;
            Active = Active;
            SelectedHall = selectedHall;
            SelectedClientUser = selectedClientUser;
            HallsForSelect = hallsForSelect;
            ClientUsersForSelect = clientUsersForSelect;
            CoachUserSelects = coachUserSelects;
        }

        public async Task RebuildAsync(AppServices appServices)
        {
            var userService = appServices.UserService;
            var hallService = appServices.HallService;

            for (int i = 0; i < CoachUserSelects?.Count; i++)
            {
                CoachUserSelects[i].Model = await userService.GetUserByIdAsync(CoachUserSelects[i].Model.Id);
            }

            for (int i = 0; i < ClientUsersForSelect?.Count; i++)
            {
                ClientUsersForSelect[i] = await userService.GetUserByIdAsync(ClientUsersForSelect[i].Id);
            }

            for (int i = 0; i < HallsForSelect?.Count; i++)
            {
                HallsForSelect[i] = await hallService.GetHallByIdAsync(HallsForSelect[i].Id);
            }

            SelectedHall = await hallService.GetHallByIdAsync(SelectedHall.Id);
            SelectedClientUser = await userService.GetUserByIdAsync(SelectedClientUser.Id);
        }
    }
}
