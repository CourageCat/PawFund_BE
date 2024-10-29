using PawFund.Domain.Abstractions.Entities;

namespace PawFund.Domain.Entities;

public class Message : DomainEntity<Guid>
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }
    public virtual Account Sender { get; set; }
    public virtual Account Receiver { get; set; }
}
