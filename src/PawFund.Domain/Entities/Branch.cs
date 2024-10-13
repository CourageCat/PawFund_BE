using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace PawFund.Domain.Entities;

public class Branch : DomainEntity<Guid>
{
    public Branch() { }

    public Branch(string name, string phoneNumberOfBranch, string emailOfBranch, string description, string numberHome, string streetName, string ward, string district, string province, string postalCode, DateTime createdDate, DateTime modifiedDate, bool isDeleted )
    {
        Name = name;
        PhoneNumberOfBranch = phoneNumberOfBranch;
        EmailOfBranch = emailOfBranch;
        Description = description;
        NumberHome = numberHome;
        StreetName = streetName;
        Ward = ward;
        District = district;
        Province = province;
        PostalCode = postalCode;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsDeleted = isDeleted;
    }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumberOfBranch { get; set; } = string.Empty;
    public string EmailOfBranch { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string NumberHome { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    [ForeignKey("Branch_Account")]
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }
    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<Cat> Cats { get; set; }

    public static Branch CreateBranch(string name, string phoneNumberOfBranch, string emailOfBranch, string description, string numberHome, string streetName, string ward, string district, string province, string postalCode, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
    {
        return new Branch(name, phoneNumberOfBranch, emailOfBranch, description, numberHome, streetName, ward, district, province, postalCode, createdDate, modifiedDate, isDeleted);
    }

    public void UpdateBranch(string name, string phoneNumberOfBranch, string emailOfBranch, string description, string numberHome, string streetName, string ward, string district, string province, string postalCode, DateTime createdDate, DateTime modifiedDate, bool isDeleted)
    {
        Name = name;
        PhoneNumberOfBranch = phoneNumberOfBranch;
        EmailOfBranch = emailOfBranch;
        Description = description;
        NumberHome = numberHome;
        StreetName = streetName;
        Ward = ward;
        District = district;
        Province = province;
        PostalCode = postalCode;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsDeleted = isDeleted;
    }
}
