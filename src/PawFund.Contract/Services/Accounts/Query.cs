using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Accounts.Response;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Contract.Services.Accounts
{
    public static class Query
    {
        public record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;

        public record GetUsersQueryHandler(int PageIndex,
         int PageSize,
         Filter.AccountFilter FilterParams,
         string[] SelectedColumns)
         : IQuery<PagedResult<UsersResponse>>;
    }
}
