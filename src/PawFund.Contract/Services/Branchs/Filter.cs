using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.Branchs;

public static class Filter
{
    public record BranchFilter(Guid? Id, string? Name, string? PhoneNumberOfBranch, string? EmailOfBranch, string? Description, string? NumberHome, string? StreetName, string? Ward, string? District, string? Province, string? PostalCode, Guid? AccountId);
}
