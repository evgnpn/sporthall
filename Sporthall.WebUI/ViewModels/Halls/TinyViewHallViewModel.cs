using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class TinyViewHallViewModel : HallViewModel
    {
        public int Id { get; set; }
        public TinyViewHallViewModel()
        {
        }
        public TinyViewHallViewModel(Hall hall) : base(hall)
        {
            Id = hall.Id;
        }
    }
}
