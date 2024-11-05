using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Accounts;
public static class Filter
{
    public record AccountFilter(Guid? Id, string? FirstName, bool? IsDeleted, RoleType? RoleType);

    public record AccountsFilter(Guid? Id, string? FirstName, string?LastName, bool? IsDeleted);

    public record AccountsDonateFilter(Guid? Id, string? FirstName, string? LastName, bool? IsDeleted);

}
