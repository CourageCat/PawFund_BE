using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Authentications;

public static class Command
{
    public record RegisterCommand
        (string FirstName, 
        string LastName, 
        string Email, 
        string PhoneNumber, 
        string Password) 
        : ICommand;
    public record VerifyEmailCommand(string Email) : ICommand;
    public record ForgotPasswordEmailCommand(string Email): ICommand;
    public record ForgotPasswordOtpCommand(string Email, string Otp) : ICommand;
    public record ForgotPasswordChangeCommand(string Email, string Password, string Otp) : ICommand;

}