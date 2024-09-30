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
    public class VolunteerApplicationConfiguration : IEntityTypeConfiguration<VolunteerApplication>
    {
        public void Configure(EntityTypeBuilder<VolunteerApplication> builder)
        {
            builder.ToTable("VolunteerApplications");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Status)
                .IsRequired();
        }
    }
}
