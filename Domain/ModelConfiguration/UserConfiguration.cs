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
            }
        );
    }
}
