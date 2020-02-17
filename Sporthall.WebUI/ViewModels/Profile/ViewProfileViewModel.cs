using Sporthall.Core.Entities;
using Sporthall.WebUI.ViewModels.Workers.CoachUsers;
using Sporthall.WebUI.ViewModels.Workers.Managers;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Profile
{
    public class ViewProfileViewModel : ProfileViewModel
    {
        public string Id { get; set; }
        public List<SoloTraining> SoloTrainings { get; set; }
        public List<GroupTraining> GroupTrainings { get; set; }
        public ViewManagerInfoViewModel ViewManagerInfoViewModel { get; set; }
        public ViewCoachInfoViewModel ViewCoachInfoViewModel { get; set; }
    }
}
