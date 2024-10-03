
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.EventActivities
{
    public static class Command
    {
        public record CreateEventActivityCommand(string Name, int NumberOfVolunteer, DateTime Time, string Desciption, bool Status, Guid EventId) : ICommand;
    }
}
