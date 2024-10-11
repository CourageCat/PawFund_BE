using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.VolunteerApplication
{
    public static class Command
    {
        public record CreateVolunteerApplication(string listActivity) : ICommand;
    }
}
