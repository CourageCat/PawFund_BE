using PawFund.Contract.Abstractions.Message;
namespace PawFund.Contract.Services.Branchs;

public static class Query
{
    public record GetBranchByIdQuery(Guid Id) : IQuery<Response.BranchResponse>;
}
