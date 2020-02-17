using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Solo
{
    public class ViewSoloTrainingViewModel : SoloTrainingViewModel
    {
        public int Id { get; set; }
        public Hall Hall { get; set; }
        public User ClientUser { get; set; }
        public List<User> CoachUsers { get; set; }
        public ViewSoloTrainingViewModel()
        {
        }
        public ViewSoloTrainingViewModel(SoloTraining soloTraining, Hall hall, User clientUser, List<User> coachUsers)
            : base(soloTraining)
        {
            Id = soloTraining.Id;
            Hall = hall;
            ClientUser = clientUser;
            CoachUsers = coachUsers;
        }
    }
}
