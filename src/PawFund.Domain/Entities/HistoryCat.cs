
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class HistoryCat : DomainEntity<Guid>
    {
        public DateTime DateAdopt { get; set; }= DateTime.Now;
        [ForeignKey("HistoryCat_Cat")]
        public Guid CatId { get; set; }
        public virtual Cat Cat { get; set; }
        [ForeignKey("HistoryCat_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
