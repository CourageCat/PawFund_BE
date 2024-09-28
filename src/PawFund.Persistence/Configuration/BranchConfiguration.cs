using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.PhoneNumberOfBranch)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(p => p.EmailOfBranch)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(p => p.NumberHome)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.StreetName)
           .IsRequired()
          .HasMaxLength(30);

        builder.Property(p => p.Ward)
           .IsRequired()
           .HasMaxLength(30);

        builder.Property(p => p.District)
           .IsRequired()
           .HasMaxLength(30);

        builder.Property(p => p.Province)
           .IsRequired()
           .HasMaxLength(30);

        builder.Property(p => p.PostalCode)
           .IsRequired()
           .HasMaxLength(30);
    }
}