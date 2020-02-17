using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewModels.Trainings.Group
{
    public class EditGroupTrainingViewModel : IRebuildableAsync
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Range(2, int.MaxValue)]
        public int Capacity { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Active { get; set; }
        public Hall SelectedHall { get; set; }
        public List<Hall> HallsForSelect { get; set; }
        public List<SelectItem<User>> ClientUserSelects { get; set; }
        public List<SelectItem<User>> CoachUserSelects { get; set; }
        public EditGroupTrainingViewModel()
        {
        }
        public EditGroupTrainingViewModel(GroupTraining groupTraining,
            Hall selectedHall, List<Hall> hallsForSelect, List<SelectItem<User>> clientUserSelects, List<SelectItem<User>> coachUserSelects)
        {
            Name = groupTraining.Name;
            Description = groupTraining.Description;
            Capacity = groupTraining.Capacity;
            Date = groupTraining.Date;
            BeginTime = groupTraining.BeginTime;
            EndTime = groupTraining.EndTime;
            Active = groupTraining.Active;
            SelectedHall = selectedHall;
            HallsForSelect = hallsForSelect;
            ClientUserSelects = clientUserSelects;
            CoachUserSelects = coachUserSelects;
        }

        public async Task RebuildAsync(AppServices appServices)
        {
            for (int i = 0; i < HallsForSelect?.Count; i++)
            {
                HallsForSelect[i] = await appServices.HallService.GetHallByIdAsync(HallsForSelect[i].Id);
            }

            for (int i = 0; i < ClientUserSelects?.Count; i++)
            {
                ClientUserSelects[i].Model = await appServices.UserService.GetUserByIdAsync(ClientUserSelects[i].Model.Id);
            }

            for (int i = 0; i < CoachUserSelects?.Count; i++)
            {
                CoachUserSelects[i].Model = await appServices.UserService.GetUserByIdAsync(CoachUserSelects[i].Model.Id);
            }

            SelectedHall = await appServices.HallService.GetHallByIdAsync(SelectedHall.Id);
        }
    }
}
