using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Solo
{
    public class CreateSoloTrainingViewModel : EditSoloTrainingViewModel
    {
        public CreateSoloTrainingViewModel()
        {
        }
        public CreateSoloTrainingViewModel(
            SoloTraining soloTraining,
            Hall selectedHall, User selectedClientUser, List<Hall> hallsForSelect, List<User> clientUsersForSelect, List<SelectItem<User>> coachUserSelects)
            : base(soloTraining, selectedHall, selectedClientUser, hallsForSelect, clientUsersForSelect, coachUserSelects)
        {
        }
    }
}
