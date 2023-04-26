using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Domain.ModelConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "994015C4-E1CE-4B39-8CA0-9D814FE9FDFE",
                Name = "admin",
                NormalizedName = "ADMIN",
            },

            new IdentityRole
            {
                Id = "67C7DFAA-E8BE-4663-A898-1E9E760FB17D",
                Name = "user",
                NormalizedName = "USER",
            }
        );
    }
}
