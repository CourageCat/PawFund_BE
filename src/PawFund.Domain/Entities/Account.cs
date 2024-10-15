using PawFund.Contract.Enumarations.Authentication;
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
        string avatarUrl,
        string avatarId,
        LoginType loginType,
        RoleType roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        Password = password;
        AvatarUrl = avatarUrl;
        AvatarId = avatarId;
        LoginType = loginType;
        RoleId = roleId;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Status { get; set; } = false;
    public LoginType LoginType { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? AvatarId { get; set; }
    public RoleType RoleId { get; set; }
    [ForeignKey("RoleId")]
    public virtual RoleUser RoleUser { get; set; }

    public virtual ICollection<Branch> Branches { get; set; }
    public virtual ICollection<AdoptPetApplication> AdoptPetApplication { get; set; }

    public virtual ICollection<HistoryCat> HistoryCats { get; set; }

    public virtual ICollection<VolunteerApplicationDetail> VolunteerApplicationDetails { get; set; }
    
    public static Account CreateMemberAccountLocal
        (string firstName, string lastName, string email, string phoneNumber, string password)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1728878878/pawfund/unknown_avatar.png";
        return new Account(firstName, lastName, email, phoneNumber, false, password, avatarUrl, null, LoginType.Local, RoleType.Member);
    }

    public static Account CreateMemberAccountGoogle
        (string firstName, string lastName, string email)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1728878878/pawfund/unknown_avatar.png";
        return new Account(firstName, lastName, email, "", false, "", avatarUrl, null, LoginType.Google, RoleType.Member);
    }
}
