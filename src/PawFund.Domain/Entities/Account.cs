using PawFund.Domain.Abstractions.Entities;

namespace PawFund.Domain.Entities;

public class Account : DomainEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool Status { get; set; }
    public int RoleId { get; set; }
}
