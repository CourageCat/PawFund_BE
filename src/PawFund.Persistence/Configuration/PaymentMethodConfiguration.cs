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

        builder.HasData(
            new PaymentMethod
            {
                Id = PaymentMethodType.Cash,
                MethodName = "Cash",
            },
            new PaymentMethod
            {
                Id = PaymentMethodType.Banking,
                MethodName = "Banking"
            }
        );
    }
}