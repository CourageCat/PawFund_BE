using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{

    public class EventActivity : DomainEntity<Guid>
    {
        public EventActivity() { }

        public EventActivity(string name, int quantity, DateTime startDate, string description, bool status, Guid eventId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            Name = name;
            Quantity = quantity;
            StartDate = startDate;
            Description = description;
            Status = status;
            EventId = eventId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        [ForeignKey("EventActivity_Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<VolunteerApplicationDetail> VolunteerApplicationDetails { get; set; }

        public static EventActivity CreateEventActivity(string name, int quantity, DateTime startDate, string description, bool status, Guid eventId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new EventActivity(name, quantity, startDate, description, status, eventId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateEventActivity(string name, int quantity, DateTime startDate, string description, bool status, Guid eventId, bool isDeleted)
        {
            Name = name;
            Quantity = quantity;
            StartDate = startDate;
            Description = description;
            Status = status;
            EventId = eventId;
            ModifiedDate = DateTime.Now;
            IsDeleted = isDeleted;
        }
    }

}
