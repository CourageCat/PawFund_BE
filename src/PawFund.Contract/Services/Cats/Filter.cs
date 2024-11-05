using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.Cats;

public static class Filter
{
    public record CatAdoptFilter(CatSex? CatSex, string? Name, string? Color, bool? Sterilization, string? Age, bool? IsDeleted);
}
