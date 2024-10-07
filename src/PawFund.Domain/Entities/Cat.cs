using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities
{
    public class Cat : DomainEntity<Guid>
    {

        public Cat() { }

        public Cat(string sex, string name, int age, string breed, decimal size, string color, string description, Guid branchId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            Sex = sex;
            Name = name;
            Age = age;
            Breed = breed;
            Size = size;
            Color = color;
            Description = description;
            BranchId = branchId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

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

        public static Cat CreateCat(string sex, string name, int age, string breed, decimal size, string color, string description, Guid branchId, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
        {
            return new Cat(sex, name, age, breed, size, color, description, branchId, createdDate, modifiedDate, isDeleted);
        }

        public void UpdateCat(string sex, string name, int age, string breed, decimal size, string color, string description, Guid branchId, DateTime? createdDate, DateTime modifiedDate, bool isDeleted)
        {
            Sex = sex;
            Name = name;
            Age = age;
            Breed = breed;
            Size = size;
            Color = color;
            Description = description;
            BranchId = branchId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
        }

    }
}
