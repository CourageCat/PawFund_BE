using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Query
    {
        public record GetEventActivityByIdQuery
       (Guid Id) : IQuery<Success<EventActivityResponse>>;

        public record GetAllEventActivity(Guid Id) : IQuery<List<Respone.EventActivityResponse>>;

        public record GetApprovedEventsActivityQuery(Guid EventId) : IQuery<Success<List<Respone.EventActivityResponse>>>;
    }
}
