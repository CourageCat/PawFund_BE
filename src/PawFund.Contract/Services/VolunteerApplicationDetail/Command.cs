using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Command
    {
        public record CreateVolunteerApplicationDetailCommand(Guid eventId, List<string> listActivity, string description, Guid userId) : ICommand;

        public record ApproveVolunteerApplicationCommand(Guid detailId) : ICommand;
        public record RejectVolunteerApplicationCommand(Guid detailId, string reason) : ICommand;

    }
}
