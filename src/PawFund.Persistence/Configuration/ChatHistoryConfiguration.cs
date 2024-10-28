using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration;

public class ChatHistoryConfiguration : IEntityTypeConfiguration<ChatHistory>
{
    public void Configure(EntityTypeBuilder<ChatHistory> builder)
    {
        builder.ToTable("ChatHistories");

        builder.HasOne(ch => ch.User)
               .WithMany()
               .HasForeignKey(ch => ch.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ch => ch.ChatPartner)
               .WithMany()
               .HasForeignKey(ch => ch.ChatPartnerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
