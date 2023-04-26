using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Services.Abstractions;

public interface IUserService
{
    Task<IEnumerable<IdentityUser>> GetAllUsersAsync();

    Task<IEnumerable<string>> GetAllRolesAsync(string userName);

    Task CreateUserAndRoleAsync(IdentityUser user, string roleName);

    Task<string> GetUserNameAsync(string username);
}