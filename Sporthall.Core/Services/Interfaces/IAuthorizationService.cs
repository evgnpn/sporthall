using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IAuthorizationService
    {
        Task SignInByUsernameAndPasswordAsync(string userName, string password, bool remember);

        Task SignOutAsync();
    }
}
