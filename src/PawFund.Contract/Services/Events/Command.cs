
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Events
{
    public static class Command
    {
        public record CreateEventCommand(string Name, DateTime StartDate, DateTime EndDate, string Description, int MaxAttendees, Guid BranchId) : ICommand;
    }
}
