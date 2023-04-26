using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Domain.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasData(
            new IdentityUser
            {
                Id = "E43DA02E-DA5C-4E35-BE9A-D2487C98A910",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "SuperCompany@gmail.com",
                NormalizedEmail = "SUPERCOMPANY@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            },

            new IdentityUser
            {
                Id = "AEF18E5C-02D5-4C59-8B69-0E6C3F350A28",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "UserTest@mail.ru",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "user"),
                SecurityStamp = string.Empty
            }
        );
    }
}
