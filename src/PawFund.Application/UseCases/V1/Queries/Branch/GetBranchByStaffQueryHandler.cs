using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using static PawFund.Contract.Services.Branchs.Response;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Queries.Branch;
public sealed class GetBranchByStaffQueryHandler : IQueryHandler<Query.GetBranchByStaffQuery, Success<Response.BranchResponse>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;

    public GetBranchByStaffQueryHandler(IDPUnitOfWork dpUnitOfWork, IRepositoryBase<Account, Guid> accountRepository)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task<Result<Success<Response.BranchResponse>>> Handle(Query.GetBranchByStaffQuery request, CancellationToken cancellationToken)
    {
        //Find Account Staff
        var accountStaff = _accountRepository.FindByIdAsync(request.StaffId);
        if(accountStaff == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(request.StaffId);
        }
        var selectColumn = new[] { "Id", "Name", "PhoneNumberOfBranch", "EmailOfBranch", "Description", "NumberHome", "StreetName", "Ward", "District", "Province", "PostalCode" };
        var result = await _dpUnitOfWork.BranchRepositories.GetPagedAsync(1, 1, new Filter.BranchFilter(null, null, null, null, null, null, null, null, null, null, null, request.StaffId), selectColumn);
        if (result.Items?.Count() == 0)
        {
            throw new BranchException.BranchNotFoundOfStaffException(request.StaffId);
        }
        var branchResponse = new Response.BranchResponse
            (result.Items[0].Id, result.Items[0].Name, result.Items[0].PhoneNumberOfBranch, result.Items[0].EmailOfBranch, result.Items[0].Description, result.Items[0].NumberHome, result.Items[0].StreetName, result.Items[0].Ward, result.Items[0].District, result.Items[0].Province, result.Items[0].PostalCode);
        return Result.Success(new Success<Response.BranchResponse>(MessagesList.BranchGetBranchByIdSuccess.GetMessage().Code, MessagesList.BranchGetBranchByIdSuccess.GetMessage().Message, branchResponse));
    }
}

