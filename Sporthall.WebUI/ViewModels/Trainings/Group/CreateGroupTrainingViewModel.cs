using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Group
{
    public class CreateGroupTrainingViewModel : EditGroupTrainingViewModel
    {
        public CreateGroupTrainingViewModel()
        {
        }

        public CreateGroupTrainingViewModel(GroupTraining groupTraining,
            List<Hall> hallsForSelect, List<SelectItem<User>> clientUserSelects, List<SelectItem<User>> coachUserSelects, Hall selectedHall = null)
              : base(groupTraining, selectedHall, hallsForSelect, clientUserSelects, coachUserSelects)
        {
        }
    }
}
