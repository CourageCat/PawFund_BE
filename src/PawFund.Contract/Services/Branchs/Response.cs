namespace PawFund.Contract.Services.Branchs;

public static class Response
{
    public record BranchResponse(Guid Id, string Name, string PhoneNumberOfBranch, string EmailOfBranch, string Description, string NumberHome, string StreetName, string Ward, string District, string Province, string PostalCode);
}
