using PawFund.Contract.Abstractions.Message;
namespace PawFund.Contract.Services.HistoryCats;

public static class Query
{
    public record GetHistoryCatByIdQuery(Guid Id) : IQuery<Response.HistoryCatResponse>;
}
