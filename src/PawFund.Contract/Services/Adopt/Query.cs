using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Adopt;

public static class Query
{
    public record GetApplicationByIdQuery
        (Guid Id) : IQuery<Response.GetApplicationByIdResponse>;

    public record GetAllApplicationQuery : IQuery<Response.GetAllApplicationResponse>;

}
