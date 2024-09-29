using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Authentications;

public static class DomainEvent
{
    public record UserCreated(Guid Id, string Email) : IDomainEvent;
}
