
using PawFund.Contract.Enumarations.Event;

namespace PawFund.Contract.Services.EventActivity
{
    public class Filter
    {
        public record EventActivityFilter(string? Name, bool? Status, bool IsAscCreatedDate);
    }
}
