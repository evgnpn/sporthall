using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Workers.CoachUsers
{
    public class ViewCoachInfoViewModel : CoachInfoViewModel
    {
        public User CoachUser { get; set; }
        public List<Hall> Halls { get; set; }
        public List<SoloTraining> SoloTrainings { get; set; }
        public List<GroupTraining> GroupTrainings { get; set; }
    }
}
