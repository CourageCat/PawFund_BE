using PawFund.Contract.Enumarations.Event;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Event : DomainEntity<Guid>
    {
        public Event() { }
        public Event(string name, DateTime startDate, DateTime endDate, string description, int maxAttendees, Guid branchId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            MaxAttendees = maxAttendees;
            BranchId = branchId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxAttendees { get; set; } = 1;
        public EventStatus Status { get; set; } = EventStatus.NotStarted;

        [ForeignKey("Event_Branch")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<EventActivity> Activities { get; set; }

        public static Event CreateEvent(string name, DateTime startDate, DateTime endDate, string description, int maxAttendees, Guid branchId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new Event(name, startDate, endDate, description, maxAttendees, branchId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateEvent(string name, DateTime startDate, DateTime endDate, string description, int maxAttendees, Guid branchId, bool isDeleted)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            MaxAttendees = maxAttendees;
            BranchId = branchId;
            ModifiedDate = DateTime.Now;
            IsDeleted = isDeleted;
        }
    }
}
