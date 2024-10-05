using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class VolunteerApplicationDetail : DomainEntity<Guid>
    {
        public bool Status {  get; set; } = false;

        [ForeignKey("VolunteerApplicationDetail_VolunteerApplication")]
        public Guid VolunteerApplicationId { get; set; }
        public virtual VolunteerApplication VolunteerApplication { get; set; }

        [ForeignKey("VolunteerApplicationDetail_EventActivity")]
        public Guid EventActivityId { get; set; }
        public virtual EventActivity EventActivity { get; set; }
    }
}
