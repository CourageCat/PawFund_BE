using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration
{
    public class EventNotificationConfiguration : IEntityTypeConfiguration<EventNotification>
    {
        public void Configure(EntityTypeBuilder<EventNotification> builder)
        {
            builder.ToTable("EventNotifications");

            // Define the primary key
            builder.HasKey(en => en.Id);

            // Configure the relationship with Event
            builder.HasOne(en => en.Event)
                   .WithMany(e => e.EventNotifications)  // Assuming a collection in Event
                   .HasForeignKey(en => en.EventId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Configure the relationship with Account
            builder.HasOne(en => en.Account)
                   .WithMany(a => a.EventNotifications)  // Assuming a collection in Account
                   .HasForeignKey(en => en.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
        }
    }
}
