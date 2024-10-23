using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Accounts;
public static class Response
{
    public record UserResponse(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber, GenderType Gender);
    public record UsersResponse(Guid Id, string FirstName, bool Status);
}
