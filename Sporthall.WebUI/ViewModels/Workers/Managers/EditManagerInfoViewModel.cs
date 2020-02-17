using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewModels.Workers.Managers
{
    public class EditManagerInfoViewModel : ManagerInfoViewModel, IRebuildableAsync
    {
        public List<User> UsersForSelect { get; set; }

        public List<SelectItem<Hall>> HallSelects { get; set; }

        [Required]
        public User SelectedManagerUser { get; set; }

        public async Task RebuildAsync(AppServices appServices)
        {
            for (int i = 0; i < UsersForSelect?.Count; i++)
            {
                UsersForSelect[i] = await appServices.UserService.GetUserByIdAsync(UsersForSelect[i].Id);
            }

            for (int i = 0; i < HallSelects?.Count; i++)
            {
                HallSelects[i].Model = await appServices.HallService.GetHallByIdAsync(HallSelects[i].Model.Id);
            }

            SelectedManagerUser = await appServices.UserService.GetUserByIdAsync(SelectedManagerUser.Id);
        }
    }
}
