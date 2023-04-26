using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Helpers;

public class ApplicationUserHelper : IdentityUser
{
    public string Role { get; set; }    
}