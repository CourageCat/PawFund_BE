using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration
{
    public class EventActivityConfiguration : IEntityTypeConfiguration<EventActivity>
    {
        public void Configure(EntityTypeBuilder<EventActivity> builder)
        {
            builder.ToTable("EventActivities");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Quantity)
                .IsRequired();
        }
    }
}
