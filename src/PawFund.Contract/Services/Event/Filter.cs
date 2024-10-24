using PawFund.Contract.Enumarations.Event;

namespace PawFund.Contract.Services.Event
{
    public class Filter
    {
        public record EventFilter(string? Name, EventStatus? Status, bool IsAscCreatedDate);
    }
}
