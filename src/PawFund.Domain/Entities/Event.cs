using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Event : DomainEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxAttendees { get; set; } = 1;

        [ForeignKey("Event_Branch")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<EventActivity> Activities { get; set; }
    }
}
