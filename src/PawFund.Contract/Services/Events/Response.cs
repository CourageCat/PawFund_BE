using PawFund.Domain.Entities;

namespace PawFund.Contract.Services.Events
{
    public static class Response
    {
        public record EventResponse(string Name, DateTime StartDate, DateTime EndDate, string Description, int MaxAttendees, Branch Branch);
    }
}
