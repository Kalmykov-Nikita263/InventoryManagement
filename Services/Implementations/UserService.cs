using InventoryManagement.Helpers;
using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagement.Services.Implementations;

/// <summary>
/// Класс, предназначенный для управления пользователями
/// </summary>
public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Получает имя пользователя из БД
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<string> GetUserNameAsync(string username)
    {
        //Ищем пользователя
        var user = await _userManager.FindByNameAsync(username);

        //Если найден - возвращаем его имя, а если нет - пустую строку
        return user.UserName ?? "";
    }

    /// <summary>
    /// Метод, который создаёт пользователя и роль в БД
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="roleName">Имя роли</param>
    /// <returns>Результат создания пользователя</returns>
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

    /// <summary>
    /// Метод, который возвращает список всех ролей пользователя
    /// </summary>
    /// <param name="userName">Имя пользователя</param>
    /// <returns>Задача, которая содержит список ролей пользователя</returns>
    public async Task<IEnumerable<string>> GetAllRolesAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles != null)
                return userRoles;
        }

        return Enumerable.Empty<string>();
    }

    /// <summary>
    /// Метод, который получает список всех пользователей в системе
    /// </summary>
    /// <returns>Задача, которая содержит список всех пользователей, в том числе с их ролями</returns>
    public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
    {
        //Получаем всех пользователей
        List<IdentityUser> users = await _userManager.Users.ToListAsync();
        List<string> roles = new();

        //Добавляем роли в список roles
        foreach (var user in users)
        {
            roles.AddRange(await _userManager.GetRolesAsync(user));
        }

        //Склеиваем список пользователей со списком ролей. Получаем коллекцию объектов ApplicationUserHelper, который содержит Id, Имя, Роли и Email
        return users.Zip(roles, (user, role) => new ApplicationUserHelper
        {
            Id = user.Id,
            UserName = user.UserName,
            Role = role,
            Email = user.Email
        });
    }
}