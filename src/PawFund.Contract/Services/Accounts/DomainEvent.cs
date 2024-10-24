using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Accounts;

public static class DomainEvent
{
    public record UserEmailChanged(Guid Id, Guid UserId, string Email) : IDomainEvent;
    public record UserPasswordChanged(Guid Id, Guid UserId, string Email) : IDomainEvent;

}
