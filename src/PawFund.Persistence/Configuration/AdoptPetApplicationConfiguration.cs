using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Domain.Entities;
using System.Reflection.Emit;

namespace PawFund.Persistence.Configuration
{
    public class AdoptPetApplicationConfiguration : IEntityTypeConfiguration<AdoptPetApplication>
    {
        public void Configure(EntityTypeBuilder<AdoptPetApplication> builder)
        {
            builder.ToTable("AdoptPetApplications");

            // Define the primary key
            builder.HasKey(p => p.Id);

            // Configure the Status property
            builder.Property(p => p.Status)
                   .IsRequired();

            // Configure the relationship with Cat
            builder.HasOne(a => a.Cat)
                   .WithMany(c => c.AdoptPetApplications)
                   .HasForeignKey(a => a.CatId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Configure the relationship with Account
            builder.HasOne(a => a.Account)
                   .WithMany(ac => ac.AdoptPetApplication)
                   .HasForeignKey(a => a.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
    }
    }
}
