

using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.AdoptApplications;

namespace PawFund.Application.UseCases.V1.Events;

public class SendEmailWhenAdoptionApplicationApprovedEventHandler
    : IDomainEventHandler<DomainEvent.AdopterHasBeenApproved>,
    IDomainEventHandler<DomainEvent.AdopterHasBeenRejected>
{
    private readonly IEmailService _emailService;

    public SendEmailWhenAdoptionApplicationApprovedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(DomainEvent.AdopterHasBeenApproved notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
                    (notification.Email,
                    "Adopt application approved PawFund",
                    "EmailAdoptApplicationApproved.html", new Dictionary<string, string> {
                    { "ToEmail", notification.Email},
                    {"CatName", notification.CatName },
                    {"Link", $"https://www.google.com"}
                     });
    }

    public async Task Handle(DomainEvent.AdopterHasBeenRejected notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
                            (notification.Email,
                            "Adopt application rejected PawFund",
                            "EmailAdoptApplicationRejected.html", new Dictionary<string, string> {
                    { "ToEmail", notification.Email},
                    {"CatName", notification.CatName},
                    {"ReasonReject", notification.ReasonReject},
                    {"Link", $"https://www.google.com"}
                    });
    }
}
