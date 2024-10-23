using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
namespace PawFund.Contract.Services.Branchs;

public static class Query
{
    public record GetBranchByIdQuery(Guid Id) : IQuery<Success<Response.BranchResponse>>;

    public record GetBranchByStaffQuery(Guid StaffId) : IQuery<Success<Response.BranchResponse>>;
}
