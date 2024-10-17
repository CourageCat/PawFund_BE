using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Branch;
public sealed class GetBranchByIdQueryHandler : IQueryHandler<Query.GetBranchByIdQuery, Response.BranchResponse>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetBranchByIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Response.BranchResponse>> Handle(Query.GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branchFound = await _dpUnitOfWork.BranchRepositories.GetByIdAsync(request.Id);
        if (branchFound == null || branchFound.IsDeleted == true)
        {
            throw new BranchException.BranchNotFoundException(request.Id);
        }
        var result = new Response.BranchResponse(branchFound.Id, branchFound.Name, branchFound.PhoneNumberOfBranch, branchFound.EmailOfBranch, branchFound.NumberHome, branchFound.StreetName, branchFound.Description, branchFound.Ward, branchFound.District, branchFound.Province, branchFound.PostalCode, branchFound.AccountId);
        return Result.Success(result);
    }
}

