using PawFund.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Entities
{
    public class PaymentMethod : DomainEntity<Guid>
    {
        public string MethodName { get; set; } = string.Empty;
        public string MethodDescription { get; set; } = string.Empty;
        public virtual ICollection<Donation> Donations { get; set; }

    }
}
