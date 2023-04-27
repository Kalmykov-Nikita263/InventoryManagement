using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Helpers;

/// <summary>
/// �����-��������� Identity User. ����� ��� ����, ����� ���� ���� � ������������
/// </summary>
public class ApplicationUserHelper : IdentityUser
{
    public string Role { get; set; }    
}