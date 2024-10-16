using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Event
{
    public static class Query
    {
        public record GetEventByIdQuery
        (Guid Id) : IQuery<Respone.EventResponse>;

        public record GetAllEvent() : IQuery<List<Respone.EventResponse>>;

        public record GetAllEventNotApproved() : IQuery<List<Respone.EventResponse>>;
    }
}
