using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using static PawFund.Contract.Services.Branchs.Filter;
using static PawFund.Contract.Services.Branchs.Response;
namespace PawFund.Contract.Services.Branchs;

public static class Query
{
    public record GetBranchByIdQuery(Guid Id) : IQuery<Success<BranchResponse>>;

    public record GetBranchByStaffQuery(Guid StaffId) : IQuery<Success<BranchResponse>>;

    public record GetAllBranchesQuery(int PageIndex,
        int PageSize,
        BranchFilter FilterParams,
        string[] SelectedColumns) : IQuery<Success<PagedResult<BranchResponse>>>;
}
