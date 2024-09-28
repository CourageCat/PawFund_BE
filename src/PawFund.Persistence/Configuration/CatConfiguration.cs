
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration
{
    public class CatConfiguration : IEntityTypeConfiguration<Cat>
    {
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.ToTable("Cats");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Sex)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Age)
                .IsRequired();

            builder.Property(p => p.Breed)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Size)
                .IsRequired();

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1500);

        }
    }
}
