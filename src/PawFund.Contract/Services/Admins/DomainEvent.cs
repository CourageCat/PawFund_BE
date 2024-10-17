using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Admin;

public static class DomainEvent
{
    public record UserBan(Guid Id, string Email, string Reason) : IDomainEvent;
    public record UserUnBan(Guid Id, string Email) : IDomainEvent;
}
