using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Command
    {
        public record FormRegisterVolunteer(Guid eventId, string listActivity, string description) :  ICommand;
        public record CreateVolunteerApplicationDetail(FormRegisterVolunteer form ,Guid userId) : ICommand;

        public record ApproveVolunteerApplication(Guid detailId) : ICommand;
        public record RejectVolunteerApplication(Guid detailId, string reason) : ICommand;

    }
}
