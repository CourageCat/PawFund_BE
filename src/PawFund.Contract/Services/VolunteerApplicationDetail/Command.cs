using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Command
    {
        public record FormRegisterVolunteerCommand(Guid eventId, string listActivity, string description) :  ICommand;
        public record CreateVolunteerApplicationDetailCommand(FormRegisterVolunteerCommand form ,Guid userId) : ICommand;

        public record ApproveVolunteerApplicationCommand(Guid detailId) : ICommand;
        public record RejectVolunteerApplicationCommand(Guid detailId, string reason) : ICommand;

    }
}
