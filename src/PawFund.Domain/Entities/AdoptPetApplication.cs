using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace PawFund.Domain.Entities
{
    public class AdoptPetApplication : DomainEntity<Guid>
    {
        public DateTime AdoptDate {  get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Status { get; set; }
        public bool IsFinalized { get; set; }
        public string AdoptProfile {  get; set; }
        [ForeignKey("AdoptPetApplication_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; } = new Account();
        [ForeignKey("AdoptPetApplication_Cat")]
        public Guid CatId { get; set; }
        public virtual Cat Cat { get; set; } = new Cat();
    }
}
