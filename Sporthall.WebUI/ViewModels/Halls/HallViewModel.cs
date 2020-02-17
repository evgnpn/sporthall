using Sporthall.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Halls
{
    public class HallViewModel
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Active { get; set; }
        public HallViewModel()
        {
        }
        protected HallViewModel(Hall hall)
        {
            Name = hall.Name;
            Address = hall.Address;
            PhoneNumber = hall.PhoneNumber;
            Description = hall.Description;
            Active = hall.Active;
        }
    }
}
