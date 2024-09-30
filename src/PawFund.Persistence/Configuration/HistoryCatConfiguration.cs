
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration
{
    public class HistoryCatConfiguration : IEntityTypeConfiguration<HistoryCat>
    {
        public void Configure(EntityTypeBuilder<HistoryCat> builder)
        {
            builder.ToTable("HistoryCats");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreatedDate)
                .IsRequired();

            // Configure the relationship with Cat
            builder.HasOne(hc => hc.Cat)
                   .WithMany(c => c.HistoryCats)
                   .HasForeignKey(hc => hc.CatId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Configure the relationship with Account
            builder.HasOne(hc => hc.Account)
                   .WithMany(ac => ac.HistoryCats)
                   .HasForeignKey(hc => hc.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
        }
    }
}
