using PawFund.Contract.Enumarations.PaymentMethod;

namespace PawFund.Contract.Services.Donates;

public static class Filter
{
    public record DonateFilter(PaymentMethodType? PaymentMethodType, int? MinAmount, int? MaxAmount, Guid? UserId);
}
