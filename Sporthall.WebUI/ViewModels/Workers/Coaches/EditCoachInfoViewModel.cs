using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewModels.Workers.CoachUsers
{
    public class EditCoachInfoViewModel : CoachInfoViewModel, IRebuildableAsync
    {
        public User SelectedUser { get; set; }
        public List<User> UsersForSelect { get; set; }
        public List<SelectItem<Hall>> HallSelects { get; set; }

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

            SelectedUser = await appServices.UserService.GetUserByIdAsync(SelectedUser.Id);
        }
    }
}
