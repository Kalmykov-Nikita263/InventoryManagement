using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Helpers;

/// <summary>
/// Класс-наследник Identity User. Нужен для того, чтобы была роль у пользователя
/// </summary>
public class ApplicationUserHelper : IdentityUser
{
    public string Role { get; set; }    
}