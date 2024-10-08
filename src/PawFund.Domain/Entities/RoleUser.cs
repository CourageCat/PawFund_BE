namespace PawFund.Domain.Entities;

public class RoleUser
{
    public RoleUser()
    { }

    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public virtual ICollection<Account> Accounts { get; set; }
}
