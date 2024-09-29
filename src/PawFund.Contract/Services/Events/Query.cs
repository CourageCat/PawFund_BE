
using PawFund.Contract.Abstractions.Message;
using static PawFund.Contract.Services.Events.Response;

namespace PawFund.Contract.Services.Events
{
    public static class Query
    {
        public record GetEventById(Guid Id) : IQuery<EventResponse>;
    }
}
