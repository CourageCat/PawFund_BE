using Microsoft.EntityFrameworkCore;
using PawFund.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Entities
{
    public class DonationEvent : DomainEntity<Guid>
    {
        public decimal Amount { get; set; } = 0;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("DonationEvent_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DonationEvent_Event")]
        // Prevent cascade delete
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }

        [ForeignKey("DonationEvent_PaymentMethod")]
        public Guid PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
