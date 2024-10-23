using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Queries.User;

public sealed class GetUserProfileQueryHandler : IQueryHandler<Query.GetUserProfileQuery, Success<Response.UserResponse>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetUserProfileQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Success<Response.UserResponse>>> Handle(Query.GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var selectColumn = new[] { "Id", "LoginType", "FirstName", "LastName", "Email", "PhoneNumber", "Status", "RoleId", "Gender", "IsDeleted" };
        var result = await _dpUnitOfWork.AccountRepositories.GetPagedAsync(1, 1, new Filter.AccountFilter(request.UserId, "", false, Contract.Enumarations.Authentication.RoleType.Member), selectColumn);
        if (result.Items?.Count() == 0) {
            throw new AccountNotFoundException();
        }
        var userResponse = new Response.UserResponse
            (result.Items[0].Id, result.Items[0].FirstName, result.Items[0].LastName, result.Items[0].Email, result.Items[0].PhoneNumber, result.Items[0].Gender, result.Items[0].LoginType);
        return Result.Success(new Success<Response.UserResponse>(MessagesList.AccountGetInfoProfileSuccess.GetMessage().Code, MessagesList.AccountGetInfoProfileSuccess.GetMessage().Message, userResponse));
    }
}
