using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.VolunteerApplicationDetail;

namespace PawFund.Application.UseCases.V1.Events
{
    public class SendMailWhenApprovalVolunteerApplicationDetail : IDomainEventHandler<DomainEvent.ApproveSendMail>,
        IDomainEventHandler<DomainEvent.RejectSendMail>
    {
        private readonly IEmailService _emailService;

        public SendMailWhenApprovalVolunteerApplicationDetail(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(DomainEvent.ApproveSendMail notification, CancellationToken cancellationToken)
        {
            await _emailService.SendMailAsync
           (notification.Email,
           "Register Volunteer PawFund",
           "EmailVolunteerApprove.html", new Dictionary<string, string> {
            { "ToEmail", notification.Email},
            {"ActivityName", notification.ActivityName},
       });
        }

        public async Task Handle(DomainEvent.RejectSendMail notification, CancellationToken cancellationToken)
        {
            await _emailService.SendMailAsync
           (notification.Email,
           "Register Volunteer PawFund",
           "EmailVolunteerReject.html", new Dictionary<string, string>
           {
               { "ToEmail", notification.Email },
               { "ActivityName", notification.ActivityName },
               { "Reason", notification.Reason },
           });
        }
    }
}
