using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using static PawFund.Contract.Services.Accounts.Response;

namespace PawFund.Contract.Services.Accounts
{
    public static class Query
    {
        public record GetUserProfileQuery(Guid UserId) : IQuery<Success<UserResponse>>;

        public record GetUsersQueryHandler(int PageIndex,
         int PageSize,
         Filter.AccountFilter FilterParams,
         string[] SelectedColumns)
         : IQuery<PagedResult<UsersResponse>>;
    }
}
