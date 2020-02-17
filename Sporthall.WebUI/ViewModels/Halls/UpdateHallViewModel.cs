using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class UpdateHallViewModel : EditHallViewModel
    {
        public int Id { get; set; }
        public UpdateHallViewModel()
        {
        }
        public UpdateHallViewModel(Hall hall, List<SelectItem<HallSchedule>> hallScheduleSelects) 
            : base(hall, hallScheduleSelects)
        {
            Id = hall.Id;
        }
    }
}
