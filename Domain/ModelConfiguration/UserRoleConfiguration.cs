using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Domain.ModelConfiguration;

/// <summary>
/// Класс-конфигурация для того, чтобы понимать кому принадлежит роль
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        //Сопоставляем Id роли с Id пользователя
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "994015C4-E1CE-4B39-8CA0-9D814FE9FDFE",
                UserId = "E43DA02E-DA5C-4E35-BE9A-D2487C98A910"
            },

            new IdentityUserRole<string>
            {
                RoleId = "67C7DFAA-E8BE-4663-A898-1E9E760FB17D",
                UserId = "AEF18E5C-02D5-4C59-8B69-0E6C3F350A28"
            }
        );
    }
}
