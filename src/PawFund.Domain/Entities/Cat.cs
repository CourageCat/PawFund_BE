using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Cat : DomainEntity<Guid>
    {
        public string Sex {  get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; } = 1;
        public string Breed {  get; set; } = string.Empty;
        public decimal Size { get; set; } = 0;
        public string Color {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Cat_Branch")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<AdoptPetApplication> AdoptPetApplications { get; set; }
        public virtual ICollection<HistoryCat> HistoryCats { get; set; }
    }
}
