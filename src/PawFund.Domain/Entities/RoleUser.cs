using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Domain.Entities;

public class RoleUser
{
    public RoleUser()
    { }

    public RoleType Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public virtual ICollection<Account> Accounts { get; set; }
}
