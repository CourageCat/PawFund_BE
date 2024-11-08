
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Enumarations.VolunteerApplication;
using MediatR;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Domain.Abstractions.Dappers;
using Microsoft.VisualBasic;
using static PawFund.Domain.Exceptions.VolunteerApplicationException;
using static PawFund.Domain.Exceptions.AccountException;
using static PawFund.Domain.Exceptions.EventActivityException;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{
    public sealed class ApproveVolunteerApplicationCommandHandler : ICommandHandler<Command.ApproveVolunteerApplicationCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _accountRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public ApproveVolunteerApplicationCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _accountRepository = accountRepository;
            _eventActivityRepository = eventActivityRepository;
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.ApproveVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            // Find volunteer application
            var existVolunteerApplication = await _volunteerApplicationDetailRepository.FindByIdAsync(request.detailId);

            if (existVolunteerApplication == null)
            {
                throw new VolunteerApplicationNotFoundException(request.detailId);
            }

            // Get related event activity
            var eventActivity = await _eventActivityRepository.FindByIdAsync(existVolunteerApplication.EventActivityId);

            if (eventActivity == null)
            {
                throw new EventActivityNotFoundException(existVolunteerApplication.EventActivityId);
            }

            // If rejected, cannot approve
            if (existVolunteerApplication.Status == VolunteerApplicationStatus.Rejected)
            {
                throw new VolunteerApplicationAlreadyRejectException();
            }

            // Change status to approved
            existVolunteerApplication.UpdateVolunteerApplication(VolunteerApplicationStatus.Approved, null);
            await _efUnitOfWork.SaveChangesAsync();

            // Update volunteer count
            eventActivity.NumberOfVolunteer += 1;
            _eventActivityRepository.Update(eventActivity);
            await _efUnitOfWork.SaveChangesAsync();

            // Check if volunteer slots are full
            if (eventActivity.NumberOfVolunteer == eventActivity.Quantity)
            {
                var allVolunteerApplications = await _efUnitOfWork.VolunteerApplicationDetail.FindAllAsync(existVolunteerApplication.EventActivityId);

                if (allVolunteerApplications.Any())
                {
                    string reason = "Number of volunteers is full now";

                    foreach (var item in allVolunteerApplications)
                    {
                        item.UpdateVolunteerApplication(VolunteerApplicationStatus.Rejected, reason);

                        // Send rejection email
                        await _publisher.Publish(new DomainEvent.RejectSendMail(
                            Guid.NewGuid(),
                            reason,
                            item.Account.Email,
                            eventActivity.Name), cancellationToken);
                    }
                    await _efUnitOfWork.SaveChangesAsync();
                }
            }

            // Get account information
            var account = await _accountRepository.FindByIdAsync(existVolunteerApplication.AccountId);
            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            // Send approval email
            await _publisher.Publish(new DomainEvent.ApproveSendMail(
                Guid.NewGuid(),
                account.Email,
                eventActivity.Name), cancellationToken);

            return Result.Success(new Success(
                MessagesList.ApproveVolunteerApplicationSuccessfully.GetMessage().Code,
                MessagesList.ApproveVolunteerApplicationSuccessfully.GetMessage().Message));
            }

        }
    }
