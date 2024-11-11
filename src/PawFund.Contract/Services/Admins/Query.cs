using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Accounts.Response;
using static PawFund.Contract.Services.Admin.Response;

namespace PawFund.Contract.Services.Admin
{
    public static class Query
    {
        // public record GetUsersQueryHandler(int PageIndex,
        //int PageSize,
        //Filter.AccountsDonateFilter FilterParams,
        //string[] SelectedColumns)
        //: IQuery<Success<PagedResult<UsersResponse>>>;
        public record GetDashboardQuery(int Year) : IQuery<Success<DashboardResponse>>;
        public record GetUsersByYearQuery(int Year) : IQuery<Success<UsersByYearResponse>>;
        public record GetUsersByYearAndMonthQuery(int Year, int Month) : IQuery<Success<UsersByYearAndMonthResponse>>;
    }
}
