using PawFund.Contract.Abstractions.Shared;

namespace PawFund.Contract.Services.Cats;

public static class Response
{
    public record CatResponse(Guid Id, string Sex, string Name, string Age, string Breed, decimal Weight, string Color, string Description);
    //public record CatsResponse(PagedResult<);
}
