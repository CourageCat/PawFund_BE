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

    public record VerifyEmail(string Email) : ICommand;
}
