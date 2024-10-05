using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace PawFund.Domain.Entities
{
    public class AdoptPetApplication : DomainEntity<Guid>
    {
        public DateTime? MeetingDate { get; set; }
        public int Status { get; set; } = 0;
        public bool IsFinalized { get; set; } = false;
        public string Description { get; set; } = string.Empty;
        [ForeignKey("AdoptPetApplication_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        [ForeignKey("AdoptPetApplication_Cat")]
        public Guid CatId { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
