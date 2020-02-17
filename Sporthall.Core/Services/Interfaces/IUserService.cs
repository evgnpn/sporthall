using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user, string password);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(User user);

        Task ChangePasswordAsync(User user, string currPass, string newPass);

        Task<User> GetUserByIdAsync(string id);

        Task<User> GetUserByUserName(string userName);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetUsersByFilterAsync(Expression<Func<User, bool>> predicate);

        Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        Task<IEnumerable<User>> GetUsersInRoleAsync(string role);
    }
}
