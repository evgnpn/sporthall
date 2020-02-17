using Sporthall.Core.Services;
using System.Threading.Tasks;

namespace Sporthall.WebUI.ViewModels
{
    public interface IRebuildable
    {
        void Rebuild(AppServices appServices);
    }

    public interface IRebuildableAsync
    {
        Task RebuildAsync(AppServices appServices);
    }
}
