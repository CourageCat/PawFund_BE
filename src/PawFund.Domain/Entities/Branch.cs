using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities;

public class Branch : DomainEntity<Guid>
{
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

}
