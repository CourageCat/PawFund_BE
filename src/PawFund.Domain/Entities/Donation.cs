using PawFund.Contract.Enumarations.PaymentMethod;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Donation : DomainEntity<Guid>
    {
        public Donation() { }
        public Donation(int amount, string description, long orderId, Guid accountId, PaymentMethodType paymentMethodId)
        {
            Amount = amount;
            Description = description;
            OrderId = orderId;
            AccountId = accountId;
            PaymentMethodId = paymentMethodId;
        }

        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public long OrderId { get; set; }

        [ForeignKey("Donation_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("Donation_PaymentMethod")]
        public PaymentMethodType PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
       
        public static Donation CreateDonationBanking(int amount, string description, long orderId, Guid accountId)
        {
            return new Donation(amount, description, orderId, accountId, PaymentMethodType.Banking);
        }
    }
}
