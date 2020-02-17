using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using System.Threading.Tasks;

namespace Sporthall.Core.Identity.Managers
{
    public interface IManualRoleStore : IRoleStore<Role>
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
