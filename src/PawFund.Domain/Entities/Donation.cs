using PawFund.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Entities
{
    public class Donation : DomainEntity<Guid>
    {
        public decimal Amount { get; set; } = 0;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Donation_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("Donation_PaymentMethod")]
        public Guid PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
