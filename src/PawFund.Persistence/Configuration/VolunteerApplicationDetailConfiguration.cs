using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Persistence.Configuration
{
    public class VolunteerApplicationDetailConfiguration : IEntityTypeConfiguration<VolunteerApplicationDetail>
    {
        public void Configure(EntityTypeBuilder<VolunteerApplicationDetail> builder)
        {
            // Set table name
            builder.ToTable("VolunteerApplicationDetails");

            // Define the primary key
            builder.HasKey(vad => vad.Id);

            // Configure the Status property
            builder.Property(vad => vad.Status)
                   .IsRequired();

            // Configure the Description property
            builder.Property(vad => vad.Description)
                   .IsRequired()
                   .HasMaxLength(1000); // Optional: You can set a max length if needed

            // Configure the ReasonReject property
            builder.Property(vad => vad.ReasonReject)
                   .HasMaxLength(1000); // Optional: Set max length for the rejection reason

            // Configure the relationship with EventActivity
            builder.HasOne(vad => vad.EventActivity)
                   .WithMany(ea => ea.VolunteerApplicationDetails) // Assuming EventActivity has a collection
                   .HasForeignKey(vad => vad.EventActivityId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure the relationship with Account
            builder.HasOne(vad => vad.Account)
                   .WithMany(a => a.VolunteerApplicationDetails) // Assuming Account has a collection
                   .HasForeignKey(vad => vad.AccountId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
