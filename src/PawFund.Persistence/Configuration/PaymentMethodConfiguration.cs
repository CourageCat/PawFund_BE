using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawFund.Contract.Enumarations.PaymentMethod;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Configuration;

internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethod");

        builder.HasKey(p => p.Id);


    }
}