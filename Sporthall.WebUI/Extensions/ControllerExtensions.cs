using Microsoft.AspNetCore.Mvc;
using Sporthall.Core.Services;

namespace Sporthall.WebUI.Extensions
{
    public static class ControllerExtensions
    {
        public static AppServices GetAppServices(this Controller controller)
        {
            return (AppServices)controller.HttpContext.RequestServices.GetService(typeof(AppServices));
        }

        public static T GetRequiredService<T>(this Controller controller)
        {
            return (T)controller.HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}
