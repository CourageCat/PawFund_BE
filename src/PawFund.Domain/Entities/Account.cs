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
        GenderType gender,
        string cropAvatarUrl,
        string cropAvatarId,
        string fullAvatarUrl,
        string fullAvatarId,
        LoginType loginType,
        RoleType roleId
        )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Gender = gender;
        CropAvatarUrl = cropAvatarUrl;
        CropAvatarId = cropAvatarId;
        FullAvatarUrl = fullAvatarUrl;
        FullAvatarId = fullAvatarId;
        LoginType = loginType;
        RoleId = roleId;
        IsDeleted = false;
    }

    public Account
        (Guid id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        bool status,
        string password,
        GenderType gender,
        string cropAvatarUrl,
        string cropAvatarId,
        string fullAvatarUrl,
        string fullAvatarId,
        LoginType loginType,
        RoleType roleId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Gender = gender;
        CropAvatarUrl = cropAvatarUrl;
        CropAvatarId = cropAvatarId;
        FullAvatarUrl = fullAvatarUrl;
        FullAvatarId = fullAvatarId;
        LoginType = loginType;
        RoleId = roleId;
        IsDeleted = false;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public LoginType LoginType { get; set; }
    public string Password { get; set; } = string.Empty;
    public GenderType Gender { get; set; }
    public string? CropAvatarUrl { get; set; }
    public string? CropAvatarId { get; set; }
    public string? FullAvatarUrl { get; set; }
    public string? FullAvatarId { get; set; }
    public RoleType RoleId { get; set; }
    [ForeignKey("RoleId")]
    public virtual RoleUser RoleUser { get; set; }

    public virtual ICollection<Branch> Branches { get; set; }
    public virtual ICollection<AdoptPetApplication> AdoptPetApplication { get; set; }

    public virtual ICollection<HistoryCat> HistoryCats { get; set; }
    public virtual ICollection<Donation> Donations { get; set; }
    public virtual ICollection<VolunteerApplicationDetail> VolunteerApplicationDetails { get; set; }
    public virtual ICollection<Message> SentMessages { get; set; }
    public virtual ICollection<Message> ReceivedMessages { get; set; }

    public static Account CreateMemberAccountLocal
        (string firstName, string lastName, string email, string phoneNumber, string password, GenderType gender)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account(firstName, lastName, email, phoneNumber, false, password, gender, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Member);
    }


    public static Account CreateMemberAccountGoogle
        (string firstName, string lastName, string email, GenderType gender)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account(firstName, lastName, email, "", false, "", gender, avatarUrl, "", avatarUrl, "", LoginType.Google, RoleType.Member);
    }

    public static Account CreateAdminAccount
       (string email, string password)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account("Admin", "", email, "", false, password, GenderType.Male, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Admin);
    }

    public static Account CreateStaffAssistant
      (string email, string password)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account("Staff assistant", "", email, "", false, password, GenderType.Male, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Staff);
    }

    public static Account CreateStaffBot
    (string email, string password)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account("Staff bot", "", email, "", false, password, GenderType.Male, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Staff);
    }

    public static Account CreateStaffAccount
       (string password, string emailOfBranch, string branchName)
    {
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1728878878/pawfund/unknown_avatar.png";
        return new Account(Guid.NewGuid(), "Staff", $"{branchName}", $"{emailOfBranch.ToLower().Substring(0, emailOfBranch.IndexOf("@"))}staff@pawfund.com", "", false, password, GenderType.Male, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Staff);
        string avatarUrl = "https://res.cloudinary.com/dilv5n8yb/image/upload/v1730774633/pawfund/yreeouhlcp33op9pesbz.png";
        return new Account(Guid.NewGuid(), "Staff", $"{branchName}", $"{emailOfBranch.ToLower().Substring(0, emailOfBranch.IndexOf("@"))}staff@pawfund.com", "", false, password, GenderType.Male, avatarUrl, "", avatarUrl, "", LoginType.Local, RoleType.Staff);
    }

    public void UpdateAvatarProfileUser(string cropAvatarUrl, string cropAvatarId, string fullAvatarUrl, string fullAvatarId)
    {
        CropAvatarUrl = cropAvatarUrl;
        CropAvatarId = cropAvatarId;
        FullAvatarUrl = fullAvatarUrl;
        FullAvatarId = fullAvatarId;
    }

    public void UpdateInfoProfileUser(string firstName, string lastName, string phoneNumber, GenderType gender)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Gender = gender;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }

    public void ChangeUserIsDelete(bool isDelete)
    {
        IsDeleted = isDelete;
    }
}
