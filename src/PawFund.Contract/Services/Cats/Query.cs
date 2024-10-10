using PawFund.Contract.Abstractions.Message;
namespace PawFund.Contract.Services.Cats;

public static class Query
{
    public record GetCatByIdQuery(Guid Id) : IQuery<Response.CatResponse>;
}
