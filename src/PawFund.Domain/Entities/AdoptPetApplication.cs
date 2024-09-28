using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace PawFund.Domain.Entities
{
    public class AdoptPetApplication : DomainEntity<Guid>
    {
        public DateTime AdoptDate {  get; set; }
        public bool Status { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey("AdoptPetApplication_Account")]
        public virtual Account Account { get; set; } = new Account();
        public Guid CatId { get; set; }
        [ForeignKey("AdoptPetApplication_Cat")]
        public virtual Cat Cat { get; set; } = new Cat();
    }
}
