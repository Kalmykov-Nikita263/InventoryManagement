using InventoryManagement.Helpers;
using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagement.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> GetUserNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user.UserName ?? "";
    }

    public async Task CreateUserAndRoleAsync(IdentityUser user, string roleName)
    {
        if (await _roleManager.FindByNameAsync(roleName) == null)
        {
            var createdUser = await _userManager.CreateAsync(user);

            if (createdUser.Succeeded)
                MessageBox.Show("Пользователь успешно создан");

            else
                MessageBox.Show(createdUser.Errors.FirstOrDefault()?.Description);

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
            });

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                MessageBox.Show($"Роль {roleName} успешно создана");

            else
                MessageBox.Show(result.Errors.FirstOrDefault()?.Description);
        }
    }

    public async Task<IEnumerable<string>> GetAllRolesAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            if(userRoles != null)
                return userRoles;
        }

        return Enumerable.Empty<string>();
    }

    public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        List<string> roles = new();

        foreach (var user in users)
        {
            roles.AddRange(await _userManager.GetRolesAsync(user));
        }

        return users.Zip(roles, (user, role) => new ApplicationUserHelper
        {
            Id = user.Id,
            UserName = user.UserName,
            Role = role,
            Email = user.Email
        });
    }
}