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
            builder.ToTable("VolunteerApplicationDetails");

            // Define the primary key
            builder.HasKey(vad => vad.Id);

            // Configure the Status property
            builder.Property(vad => vad.Status)
                   .IsRequired();

            // Configure the relationship with VolunteerApplication
            builder.HasOne(vad => vad.VolunteerApplication)
                   .WithMany(va => va.ApplicationDetails)  // Assuming a collection in VolunteerApplication
                   .HasForeignKey(vad => vad.VolunteerApplicationId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Configure the relationship with EventActivity
            builder.HasOne(vad => vad.EventActivity)
                   .WithMany(ea => ea.VolunteerApplicationDetails)  // Assuming a collection in EventActivity
                   .HasForeignKey(vad => vad.EventActivityId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
        }
    }
}
