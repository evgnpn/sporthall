using Sporthall.Core.Entities;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Workers.Managers
{
    public class ViewManagerInfoViewModel : ManagerInfoViewModel
    {
        public User ManagerUser { get; set; }
        public List<Hall> Halls { get; set; }
    }
}
