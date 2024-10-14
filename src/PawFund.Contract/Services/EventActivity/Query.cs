using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Query
    {
        public record GetEventActivityByIdQuery
       (Guid Id) : IQuery<Respone.EventActivityResponse>;

        public record GetAllEventActivity(Guid Id) : IQuery<List<Respone.EventActivityResponse>>;
    }
}
