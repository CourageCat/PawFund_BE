using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities;

public class Account : DomainEntity<Guid>
{
    public Account()
    {
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Status { get; set; } = false;
    public string Password { get; set; } = string.Empty;

    [ForeignKey("Account_RoleUser")]
    public int RoleId { get; set; } = 3;
    public virtual RoleUser RoleUser { get; set; } = new RoleUser();

    public virtual ICollection<Branch> Branches { get; set; }
    public virtual ICollection<AdoptPetApplication> AdoptPetApplication { get; set; }

    public virtual ICollection<HistoryCat> HistoryCats { get; set; }

}
