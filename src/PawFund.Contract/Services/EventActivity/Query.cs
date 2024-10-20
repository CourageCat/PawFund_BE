using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Query
    {
        public record GetEventActivityByIdQuery
       (Guid Id) : IQuery<Respone.EventActivityResponse>;

        public record GetAllEventActivity(Guid Id) : IQuery<List<Respone.EventActivityResponse>>;

        public record GetApprovedEventsActivityQuery(Guid EventId) : IQuery<Success<List<Respone.EventActivityResponse>>>;
    }
}
