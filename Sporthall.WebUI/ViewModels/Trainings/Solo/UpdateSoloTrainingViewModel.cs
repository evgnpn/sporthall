using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Solo
{
    public class UpdateSoloTrainingViewModel : EditSoloTrainingViewModel
    {
        public int Id { get; set; }
        public UpdateSoloTrainingViewModel()
        {
        }
        public UpdateSoloTrainingViewModel(SoloTraining soloTraining,
            Hall selectedHall, User selectedClientUser, List<Hall> hallsForSelect, List<User> clientUsersForSelect, List<SelectItem<User>> coachUserSelects)
            : base(soloTraining, selectedHall, selectedClientUser, hallsForSelect, clientUsersForSelect, coachUserSelects)
        {
            Id = soloTraining.Id;
        }
    }
}
