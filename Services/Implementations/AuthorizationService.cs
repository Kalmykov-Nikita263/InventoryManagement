using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagement.Services.Implementations;

public class AuthorizationService : IAuthorizationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> LoginPasswordSignInAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
                return true;
        }

        return false;
    }
}
