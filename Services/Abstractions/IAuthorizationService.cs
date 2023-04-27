using System.Threading.Tasks;

namespace InventoryManagement.Services.Abstractions;

/// <summary>
/// Интерфейс для авторизации пользователя, а также проверки на администратора
/// </summary>
public interface IAuthorizationService
{
    Task<bool> LoginPasswordSignInAsync(string username, string password);

    Task<bool> CheckRoleIsAdministrator(string username);
}
