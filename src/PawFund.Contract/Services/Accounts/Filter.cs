using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Accounts;
public static class Filter
{
    public record AccountFilter(string? FirstName, bool? Status, RoleType? RoleType);
}
