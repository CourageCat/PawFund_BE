
using PawFund.Contract.Enumarations.Cat;
using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Xml.Linq;

namespace PawFund.Domain.Entities
{
    public class HistoryCat : DomainEntity<Guid>
    {
        public HistoryCat() { }
        
        public HistoryCat(DateTime dateAdopt, Guid catId, Guid accountId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            DateAdopt = dateAdopt;
            CatId = catId; 
            AccountId = accountId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
        public DateTime DateAdopt { get; set; }= DateTime.Now;
        [ForeignKey("HistoryCat_Cat")]
        public Guid CatId { get; set; }
        public virtual Cat Cat { get; set; }
        [ForeignKey("HistoryCat_Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public static HistoryCat CreateHistoryCat(DateTime dateAdopt, Guid catId, Guid accountId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new HistoryCat(dateAdopt, catId, accountId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateHistoryCat(DateTime dateAdopt, Guid catId, Guid accountId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            DateAdopt = dateAdopt;
            CatId = catId;
            AccountId = accountId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }
    }
}
