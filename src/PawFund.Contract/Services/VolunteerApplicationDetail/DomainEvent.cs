using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.VolunteerApplicationDetail;

public static class DomainEvent
{
    public record ApproveSendMail(Guid Id, string Email, string ActivityName) : IDomainEvent;
    public record RejectSendMail(Guid Id, string Reason, string Email, string ActivityName) : IDomainEvent;
}
