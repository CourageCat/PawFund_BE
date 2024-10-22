using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Shared;
using static PawFund.Contract.Services.AdoptApplications.Response;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Query
    {
        public record GetEventActivityByIdQuery
       (Guid Id) : IQuery<Success<EventActivityResponse>>;

        public record GetAllEventActivity(Guid Id, bool filterParams, int pageIndex, int pageSize,
            string[] selectedColumns) : IQuery<Success<PagedResult<EventActivityResponse>>>;
    }
}
