using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Group
{
    public class UpdateGroupTrainingViewModel : EditGroupTrainingViewModel
    {
        public int Id { get; set; }
        public UpdateGroupTrainingViewModel()
        {
        }

        public UpdateGroupTrainingViewModel(GroupTraining groupTraining,
            Hall selectedHall, List<Hall> hallsForSelect, List<SelectItem<User>> clientUserSelects, List<SelectItem<User>> coachUserSelects)
              : base(groupTraining, selectedHall, hallsForSelect, clientUserSelects, coachUserSelects)
        {
            Id = groupTraining.Id;
        }
    }
}
