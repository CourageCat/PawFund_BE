using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class VolunteerApplication : DomainEntity<Guid>
    {
        public bool Status {  get; set; } = false;

        [ForeignKey("VolunteerApplication_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<VolunteerApplicationDetail> ApplicationDetails { get; set; }
    }
}
