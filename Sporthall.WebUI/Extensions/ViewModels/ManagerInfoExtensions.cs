using Sporthall.Core;
using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels.Workers.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class ManagerInfoExtensions
    {
        public static ManagerInfo ToModel(this ViewManagerInfoViewModel viewManagerInfoViewModel)
        {
            ManagerInfo model = new ManagerInfo
            {
                Description = viewManagerInfoViewModel.Description,
                UserId = viewManagerInfoViewModel.ManagerUser?.Id
            };
            return model;
        }

        public static ManagerInfo ToModel(this UpdateManagerInfoViewModel updateManagerInfoViewModel)
        {
            ManagerInfo model = new ManagerInfo
            {
                Description = updateManagerInfoViewModel.Description,
                UserId = updateManagerInfoViewModel.SelectedManagerUser?.Id,
            };

            return model;
        }

        public static ManagerInfo ToModel(this CreateManagerInfoViewModel createManagerInfoViewModel)
        {
            var model = new ManagerInfo
            {
                Description = createManagerInfoViewModel.Description,
                UserId = createManagerInfoViewModel.SelectedManagerUser?.Id,
            };

            return model;
        }

        public static CreateManagerInfoViewModel ToCreateViewModel(
            this ManagerInfo manager, IUnitOfWork unitOfWork)
        {
            var model = new CreateManagerInfoViewModel
            {
                Description = manager.Description,
                SelectedManagerUser = null,
                UsersForSelect = unitOfWork.UserRepository.GetAll().ToList(),
                HallSelects = unitOfWork.HallRepository.GetAll().AsSelectItems().ToList(),
            };
            return model;
        }

        public static async Task<CreateManagerInfoViewModel> ToCreateViewModelAsync(this ManagerInfo manager, AppServices appServices)
        {
            var model = new CreateManagerInfoViewModel
            {
                Description = manager.Description,
                SelectedManagerUser = null,
                UsersForSelect = (await appServices.UserService.GetAllUsersAsync()).ToList(),
                HallSelects = (await appServices.HallService.GetAllHallsAsync()).AsSelectItems().ToList(),
            };
            return model;
        }

        public static async Task<UpdateManagerInfoViewModel> ToUpdateViewModelAsync(this ManagerInfo managerInfo, AppServices appServices)
        {
            var managerUserHalls = await appServices.ManagerService.GetManagerHallsAsync(managerInfo.UserId);
            var hallsForSelect = await appServices.HallService.GetAllHallsAsync();

            var model = new UpdateManagerInfoViewModel
            {
                Description = managerInfo.Description,
                HallSelects = hallsForSelect.AsSelectItems(managerUserHalls, new HallIdComparer()).ToList(),
                SelectedManagerUser = await appServices.ManagerService.GetManagerUserAsync(managerInfo.UserId),
            };

            return model;
        }

        public static async Task<ViewManagerInfoViewModel> ToViewViewModelAsync(this ManagerInfo managerInfo, AppServices appServices, bool includeUser = true)
        {
            if (managerInfo is null)
            {
                return null;
            }

            var managerHalls = await appServices.ManagerService.GetManagerHallsAsync(managerInfo.UserId);
            var managerUser = includeUser ? await appServices.ManagerService.GetManagerUserAsync(managerInfo.UserId) : null;

            var model = new ViewManagerInfoViewModel
            {
                Description = managerInfo.Description,
                Halls = managerHalls.ToList(),
                ManagerUser = managerUser
            };

            return model;
        }

        public static async Task<IEnumerable<ViewManagerInfoViewModel>> ToViewViewModelsAsync(this IEnumerable<ManagerInfo> managerInfos, AppServices appServices)
        {
            var list = new List<ViewManagerInfoViewModel>();

            foreach (var managerInfo in managerInfos)
            {
                list.Add(await managerInfo.ToViewViewModelAsync(appServices));
            }

            return list;
        }
    }
}
