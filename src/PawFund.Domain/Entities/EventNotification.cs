using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class EventNotification : DomainEntity<Guid>
    {
        public EventNotification() { }

        [ForeignKey("EventNotification_Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }

        [ForeignKey("EventNotification_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
