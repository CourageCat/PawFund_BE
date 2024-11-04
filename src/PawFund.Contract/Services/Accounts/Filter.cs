using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Accounts;
public static class Filter
{
    public record AccountFilter(Guid? Id, string? FirstName, bool? Status, RoleType? RoleType);

    public record AccountsFilter(Guid? Id, string? FirstName, string?LastName, bool? Status);

    public record AccountsDonateFilter(Guid? Id, string? FirstName, string? LastName, bool? Status);

}
