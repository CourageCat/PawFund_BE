using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Branch;

public sealed class GetAllBranchesQueryHandler : IQueryHandler<Query.GetAllBranchesQuery, Success<PagedResult<Response.BranchResponse>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetAllBranchesQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Success<PagedResult<Response.BranchResponse>>>> Handle(Query.GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        var listBranches = await _dpUnitOfWork.BranchRepositories.GetPagedAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);
        if(listBranches.Items.Count == 0)
        {
            throw new BranchException.BranchEmptyException();
        }
        var result = _mapper.Map<PagedResult<Response.BranchResponse>>(listBranches);
        return Result.Success(new Success<PagedResult<Response.BranchResponse>>(MessagesList.BranchGetAllBranchesSuccess.GetMessage().Code, MessagesList.BranchGetAllBranchesSuccess.GetMessage().Message, result));

    }
}
