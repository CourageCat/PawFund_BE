

using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.AdoptApplications;

namespace PawFund.Application.UseCases.V1.Events;

public class SendEmailWhenAdoptionApplicationApprovedEventHandler
    : IDomainEventHandler<DomainEvent.AdoptionHasBeenApproved>,
    IDomainEventHandler<DomainEvent.AdoptionHasBeenRejected>,
    IDomainEventHandler<DomainEvent.AdoptionHasBeenCompleted>,
    IDomainEventHandler<DomainEvent.AdoptionHasBeenRejectedOutside>
{
    private readonly IEmailService _emailService;

    public SendEmailWhenAdoptionApplicationApprovedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(DomainEvent.AdoptionHasBeenApproved notification, CancellationToken cancellationToken)
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

    public async Task Handle(DomainEvent.AdoptionHasBeenRejected notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
                            (notification.Email,
                            "Adopt application rejected PawFund",
                            "EmailAdoptApplicationRejected.html", new Dictionary<string, string> {
                    { "ToEmail", notification.Email},
                    {"CatName", notification.CatName},
                    {"ReasonReject", notification.ReasonReject},
                    });
    }

    public async Task Handle(DomainEvent.AdoptionHasBeenCompleted notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
                            (notification.Email,
                            "Adopt application completed PawFund",
                            "EmailAdoptApplicationCompleted.html", new Dictionary<string, string> {
                    { "ToEmail", notification.Email},
                    {"CatName", notification.CatName },
                             });
    }

    public async Task Handle(DomainEvent.AdoptionHasBeenRejectedOutside notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
                                    (notification.Email,
                                    "Adopt application rejected outside PawFund",
                                    "EmailAdoptApplicationRejectedOutside.html", new Dictionary<string, string> {
                    { "ToEmail", notification.Email},
                    {"CatName", notification.CatName},
                    {"ReasonReject", notification.ReasonReject},
                            });
    }
}
