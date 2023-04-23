using System.Threading.Tasks;

namespace InventoryManagement.Services.Abstractions;

public interface IAuthorizationService
{
    Task<bool> LoginPasswordSignInAsync(string username, string password);

    Task<bool> CheckRoleIsAdministrator(string username);

    Task<string> GetUserNameAsync(string username);
}
