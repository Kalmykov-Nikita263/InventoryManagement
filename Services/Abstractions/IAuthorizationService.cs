using System.Threading.Tasks;

namespace InventoryManagement.Services.Abstractions;

public interface IAuthorizationService
{
    Task<bool> LoginPasswordSignInAsync(string username, string password);
}
