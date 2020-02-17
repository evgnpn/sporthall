using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using System.Threading.Tasks;

namespace Sporthall.Core.Identity.Managers
{
    public interface IManualUserStore : IUserStore<User>
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
