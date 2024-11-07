using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.Cats;

public static class Filter
{
    public record CatAdoptFilter(CatSex? CatSex, Guid? BranchId, string? Name, string? Color, bool? Sterilization, string? Age, bool? IsDeleted);
    public record CatFilter(CatSex? CatSex, Guid? AccountId, string? Name, string? Color, bool? Sterilization, string? Age, bool? IsDeleted);
}
