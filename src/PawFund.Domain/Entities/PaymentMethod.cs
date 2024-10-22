using PawFund.Contract.Enumarations.PaymentMethod;

namespace PawFund.Domain.Entities
{
    public class PaymentMethod
    {
        public PaymentMethod()
        { }
        public PaymentMethodType Id { get; set; }
        public string MethodName { get; set; } = string.Empty;
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
