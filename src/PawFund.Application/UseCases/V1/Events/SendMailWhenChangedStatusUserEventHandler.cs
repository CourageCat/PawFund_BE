using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.Admin;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Application.UseCases.V1.Events
{
    public class SendMailWhenChangedStatusUserEventHandler : IDomainEventHandler<DomainEvent.UserBan>, IDomainEventHandler<DomainEvent.UserUnBan>
    {
        private readonly IEmailService _emailService;

        public SendMailWhenChangedStatusUserEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(DomainEvent.UserBan notification, CancellationToken cancellationToken)
        {
            await _emailService.SendMailAsync
                    (notification.Email,
                    "Banned Notification",
                    "EmailBannedUser.html", new Dictionary<string, string> {
                    {"ToEmail", notification.Email},
                    {"Reason", notification.Reason}
            });
        }


        public async Task Handle(DomainEvent.UserUnBan notification, CancellationToken cancellationToken)
        {
            await _emailService.SendMailAsync
                   (notification.Email,
                   "Banned Notification",
                   "EmailUnBannedUser.html", new Dictionary<string, string> {
                    {"ToEmail", notification.Email},
           });
        }
    }
}
