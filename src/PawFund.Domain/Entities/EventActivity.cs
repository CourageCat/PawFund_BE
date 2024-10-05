using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class EventActivity : DomainEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity {  get; set; } = 0;
        public DateTime StartDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status {  get; set; } = false;
        [ForeignKey("EventActivity_Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<VolunteerApplicationDetail> VolunteerApplicationDetails { get; set; }
    }
}
