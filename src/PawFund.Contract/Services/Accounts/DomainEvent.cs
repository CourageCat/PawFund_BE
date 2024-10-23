using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Accounts;

public static class DomainEvent
{
    public record UserEmailChanged(Guid Id, string Email) : IDomainEvent;
}
