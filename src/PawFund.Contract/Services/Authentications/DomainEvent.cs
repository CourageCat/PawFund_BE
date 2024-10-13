using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Authentications;

public static class DomainEvent
{
    public record UserCreated(Guid Id, string Email) : IDomainEvent;
    public record UserOtpChanged(Guid Id, string Email, string Otp) : IDomainEvent;
    public record UserPasswordChanged(Guid Id, string Email) : IDomainEvent;
    public record UserVerifiedEmailRegist(Guid Id, string Email) : IDomainEvent;
    public record UserCreatedWithGoogle(Guid Id, string Email) : IDomainEvent;
}
