using PawFund.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawFund.Domain.Entities;

public class Account : DomainEntity<Guid>
{
    public Account()
    {
    }

    public Account
        (string firstName,
        string lastName,
        string email,
        string phoneNumber,
        bool status,
        string password,
        int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        Password = password;
        RoleId = roleId;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Status { get; set; } = false;
    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public virtual RoleUser RoleUser { get; set; }

    public virtual ICollection<Branch> Branches { get; set; }
    public virtual ICollection<AdoptPetApplication> AdoptPetApplication { get; set; }

    public virtual ICollection<HistoryCat> HistoryCats { get; set; }

    public static Account CreateMemberAccount
        (string firstName, string lastName, string email, string phoneNumber, string password)
    {
        return new Account(firstName, lastName, email, phoneNumber, false, password, 3);
    }

}
