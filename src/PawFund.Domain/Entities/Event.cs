using Microsoft.AspNetCore.Http;
using PawFund.Contract.Enumarations.Event;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Event : DomainEntity<Guid>
    {
        public Event() { }

        public Event(string name, DateTime startDate, DateTime endDate, string description, int maxAttendees, Guid branchId, string thumbHeroUrl, string thumbHeroId, string imagesUrl, string imagesId, DateTime createdDate, DateTime modifiedDate, bool isDeleted, string reasonReject)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            MaxAttendees = maxAttendees;
            BranchId = branchId;
            ThumbHeroUrl = thumbHeroUrl;
            ThumbHeroId = thumbHeroId;
            ImagesUrl = imagesUrl;
            ImagesId = imagesId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
            ReasonReject = reasonReject;
        }


        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxAttendees { get; set; } = 1;
        public string? ReasonReject {  get; set; } = string.Empty;
        public EventStatus Status { get; set; } = EventStatus.NotStarted;
        public string? ThumbHeroUrl { get; set; }
        public string? ThumbHeroId { get; set; }
        public string? ImagesUrl { get; set; }
        public string? ImagesId { get; set; }

        [ForeignKey("Event_Branch")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<EventActivity> Activities { get; set; }

        public static Event CreateEvent(string name, DateTime startDate, DateTime endDate, string description, int maxAttendees, Guid branchId, string ThumbHeroUrl, string ThumbHeroId, string ImagesUrl, string ImagesId, DateTime createdDate, DateTime modifiedDate, bool isDeleted, string? reasonReject)
        {
            return new Event(name, startDate, endDate, description, maxAttendees, branchId, ThumbHeroUrl, ThumbHeroId, ImagesUrl, ImagesId, createdDate, modifiedDate, isDeleted, reasonReject);
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
