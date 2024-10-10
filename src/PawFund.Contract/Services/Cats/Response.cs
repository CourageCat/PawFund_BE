namespace PawFund.Contract.Services.Cats;

public static class Response
{
    public record CatResponse(Guid Id, string Sex, string Name, int Age, string Breed, decimal Size, string Color, string Description);
}
