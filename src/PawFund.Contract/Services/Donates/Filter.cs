using PawFund.Contract.Enumarations.PaymentMethod;

namespace PawFund.Contract.Services.Donates;

public static class Filter
{
    // IsDateDesc = true ? DESC : ASC
    public record DonateFilter(PaymentMethodType? PaymentMethodType, int? MinAmount, int? MaxAmount, Guid? UserId, bool? IsDateDesc);
}
