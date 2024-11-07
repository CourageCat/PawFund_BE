using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Query
    {
        public record GetEventActivityByIdQuery
       (Guid Id) : IQuery<Success<EventActivityResponse>>;

        public record GetAllEventActivity(Guid EventId,
        int PageIndex,
         int PageSize,
         Filter.EventActivityFilter FilterParams,
         string[] SelectedColumns) : IQuery<Success<PagedResult<GetEventActivityByIdDTO.ActivityDTO>>>;

        public record GetApprovedEventsActivityQuery(Guid EventId) : IQuery<Success<List<Respone.EventActivityResponse>>>;
    }
}
