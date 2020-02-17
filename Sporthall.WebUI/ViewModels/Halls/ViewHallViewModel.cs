using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class ViewHallViewModel : HallViewModel
    {
        public int Id { get; set; }
        public List<SelectItem<HallSchedule>> HallScheduleSelects { get; set; }
        public List<SoloTraining> SoloTrainings { get; set; }
        public List<GroupTraining> GroupTrainings { get; set; }
        public List<User> ManagerUsers { get; set; }
        public List<User> CoachUsers { get; set; }
        public ViewHallViewModel()
        {
        }
        public ViewHallViewModel(Hall hall, List<SelectItem<HallSchedule>> hallScheduleSelects, List<SoloTraining> soloTrainings, List<GroupTraining> groupTrainings, List<User> managerUsers, List<User> coachUsers)
            : base(hall)
        {
            Id = hall.Id;
            HallScheduleSelects = hallScheduleSelects;
            SoloTrainings = soloTrainings;
            GroupTrainings = groupTrainings;
            ManagerUsers = managerUsers;
            CoachUsers = coachUsers;
        }
    }
}
