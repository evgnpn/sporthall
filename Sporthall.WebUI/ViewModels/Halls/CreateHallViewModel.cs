using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class CreateHallViewModel : EditHallViewModel
    {
        public CreateHallViewModel()
        {
        }
        public CreateHallViewModel(Hall hall, List<SelectItem<HallSchedule>> hallScheduleSelects)
        {
            Name = hall.Name;
            Address = hall.Address;
            PhoneNumber = hall.PhoneNumber;
            Description = hall.Description;
            Active = hall.Active;
            HallScheduleSelects = hallScheduleSelects;
        }
    }
}
