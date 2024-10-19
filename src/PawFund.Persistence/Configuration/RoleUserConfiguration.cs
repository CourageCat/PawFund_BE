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

        //builder.HasData(
        //    new RoleUser
        //    {
        //        Id = RoleType.Admin,
        //        RoleName = "Admin",
        //    },
        //    new RoleUser
        //    {
        //        Id = RoleType.Staff,
        //        RoleName = "Staff",
        //    },
        //    new RoleUser
        //    {
        //        Id = RoleType.Member,
        //        RoleName = "Member"
        //    }
        //);
    }
}
