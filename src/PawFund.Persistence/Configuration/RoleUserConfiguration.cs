using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration;

public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.ToTable("RoleUsers");

        builder.HasKey(p => p.Id);

        builder.HasData(
            new RoleUser
            {
                Id = 1,
                RoleName = "Admin",
            },
            new RoleUser
            {
                Id = 2,
                RoleName = "Staff",
            },
            new RoleUser
            {
                Id = 3,
                RoleName = "Member"
            }
        );
    }
}
