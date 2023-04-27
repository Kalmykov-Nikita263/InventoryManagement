using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagement.Services.Implementations;

/// <summary>
/// Класс, который авторизует пользователя и проверит его роль
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Метод проверяет роль пользователя на администратора
    /// </summary>
    /// <param name="username">Имя пользователя, которое надо проверить</param>
    /// <returns>Задача, которая содержит результат проверки</returns>
    public async Task<bool> CheckRoleIsAdministrator(string username)
    {
        //Ищем пользователя в БД
        var user = await _userManager.FindByNameAsync(username);

        //Если мы нашли пользователя и его роль администратор
        if (user != null && await _userManager.IsInRoleAsync(user, "admin")) 
        { 
            return true;
        }

        return false;
    }

    /// <summary>
    /// Метод проверяет роль пользователя на администратора
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="password">Пароль</param>
    /// <returns>Задача, которая содержит результат входа</returns>
    public async Task<bool> LoginPasswordSignInAsync(string username, string password)
    {
        //Ищем пользователя в БД
        var user = await _userManager.FindByNameAsync(username);

        if (user != null)
        {
            //Если нашли - проверяем его логин и пароль в БД
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            //Если всё корректно - вошли
            if (result.Succeeded)
                return true;
        }

        return false;
    }
}
