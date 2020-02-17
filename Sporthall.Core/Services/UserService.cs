using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class UserService : SimpleServiceBase, IUserService
    {
        public UserService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task CreateUserAsync(User user, string password)
        {
            var identityRes = await UserManager.CreateAsync(user, password);
            ThrowIfIdentityNotSuccess(identityRes);
        }

        public async Task DeleteUserAsync(User user)
        {
            var identityRes = await UserManager.DeleteAsync(user);
            ThrowIfIdentityNotSuccess(identityRes);
        }

        public async Task ChangePasswordAsync(User user, string currPass, string newPass)
        {
            var identityRes = await UserManager.ChangePasswordAsync(user, currPass, newPass);
            ThrowIfIdentityNotSuccess(identityRes);
        }

        public async Task UpdateUserAsync(User user)
        {
            var identityRes = await UserManager.UpdateAsync(user);
            ThrowIfIdentityNotSuccess(identityRes);
        }

        public Task<User> GetUserByIdAsync(string userId)
        {
            return UserManager.FindByIdAsync(userId);
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return UnitOfWork.UserRepository.GetAllAsync();
        }

        public Task<IEnumerable<User>> GetUsersByFilterAsync(Expression<Func<User, bool>> predicate)
        {
            return UnitOfWork.UserRepository.GetByFilterAsync(predicate);
        }

        public Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return UserManager.GetUserAsync(claimsPrincipal);
        }

        public Task<User> GetUserByUserName(string userName)
        {
            return UserManager.FindByNameAsync(userName);
        }

        public async Task<IEnumerable<User>> GetUsersInRoleAsync(string roleName)
        {
            return await UserManager.GetUsersInRoleAsync(roleName);
        }
    }
}
