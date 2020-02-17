using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class EditHallViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public virtual  string Address { get; set; }
        [Required(AllowEmptyStrings = false)]
        public virtual  string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public virtual string Description { get; set; }
        public virtual bool Active { get; set; }
        public virtual List<SelectItem<HallSchedule>> HallScheduleSelects { get; set; }
        public EditHallViewModel()
        {
        }
        public EditHallViewModel(Hall hall, List<SelectItem<HallSchedule>> hallScheduleSelects)
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
