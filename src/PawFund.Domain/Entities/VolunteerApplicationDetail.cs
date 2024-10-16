using PawFund.Contract.Enumarations.VolunteerApplication;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class VolunteerApplicationDetail : DomainEntity<Guid>
    {
        public VolunteerApplicationDetail() { }

        public VolunteerApplicationDetail(VolunteerApplicationStatus status, string description, string? reasonReject, Guid eventActivityId, Guid eventId, Guid accountId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            Status = status;
            Description = description;
            ReasonReject = reasonReject;
            EventActivityId = eventActivityId;
            EventId = eventId;
            AccountId = accountId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

        public VolunteerApplicationStatus Status {  get; set; } = VolunteerApplicationStatus.Pending;
        public string Description { get; set; } = string.Empty;
        public string? ReasonReject {  get; set; } = null;
        public Guid EventId { get; set; } = Guid.Empty;

        [ForeignKey("VolunteerApplicationDetail_EventActivity")]
        public Guid EventActivityId { get; set; }
        public virtual EventActivity EventActivity { get; set; }

        [ForeignKey("VolunteerApplicationDetail_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public static VolunteerApplicationDetail createVolunteerApplication(VolunteerApplicationStatus status, string description, string? reasonReject, Guid eventActivityId, Guid eventId, Guid accountId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new VolunteerApplicationDetail(status, description, reasonReject, eventActivityId, eventId, accountId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateVolunteerApplication(VolunteerApplicationStatus status, string reasonReject)
        {
            Status = status;
            ReasonReject = reasonReject;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
