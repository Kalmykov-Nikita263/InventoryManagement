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
/// �����, ��������������� ��� ���������� ��������������
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
    /// �������� ��� ������������ �� ��
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<string> GetUserNameAsync(string username)
    {
        //���� ������������
        var user = await _userManager.FindByNameAsync(username);

        //���� ������ - ���������� ��� ���, � ���� ��� - ������ ������
        return user.UserName ?? "";
    }

    /// <summary>
    /// �����, ������� ������ ������������ � ���� � ��
    /// </summary>
    /// <param name="user">������������</param>
    /// <param name="roleName">��� ����</param>
    /// <returns>��������� �������� ������������</returns>
    public async Task CreateUserAndRoleAsync(IdentityUser user, string roleName)
    {
        if (await _roleManager.FindByNameAsync(roleName) == null)
        {
            var createdUser = await _userManager.CreateAsync(user);

            if (createdUser.Succeeded)
                MessageBox.Show("������������ ������� ������");

            else
                MessageBox.Show(createdUser.Errors.FirstOrDefault()?.Description);

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
            });

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                MessageBox.Show($"���� {roleName} ������� �������");

            else
                MessageBox.Show(result.Errors.FirstOrDefault()?.Description);
        }
    }

    /// <summary>
    /// �����, ������� ���������� ������ ���� ����� ������������
    /// </summary>
    /// <param name="userName">��� ������������</param>
    /// <returns>������, ������� �������� ������ ����� ������������</returns>
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
    /// �����, ������� �������� ������ ���� ������������� � �������
    /// </summary>
    /// <returns>������, ������� �������� ������ ���� �������������, � ��� ����� � �� ������</returns>
    public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
    {
        //�������� ���� �������������
        List<IdentityUser> users = await _userManager.Users.ToListAsync();
        List<string> roles = new();

        //��������� ���� � ������ roles
        foreach (var user in users)
        {
            roles.AddRange(await _userManager.GetRolesAsync(user));
        }

        //��������� ������ ������������� �� ������� �����. �������� ��������� �������� ApplicationUserHelper, ������� �������� Id, ���, ���� � Email
        return users.Zip(roles, (user, role) => new ApplicationUserHelper
        {
            Id = user.Id,
            UserName = user.UserName,
            Role = role,
            Email = user.Email
        });
    }
}