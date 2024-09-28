
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class HistoryCat : DomainEntity<Guid>
    {
        public DateTime DateAdopt { get; set; }= DateTime.Now;
        public Guid CatId { get; set; }
        [ForeignKey("HistoryCat_Cat")]
        public virtual Cat Cat { get; set; } = new Cat();
        public Guid AccountId { get; set; }
        [ForeignKey("HistoryCat_Account")]
        public virtual Account Account { get; set; } = new Account();
    }
}
