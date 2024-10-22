
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Enumarations.VolunteerApplication;
using MediatR;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{
    public sealed class ApproveVolunteerApplicationCommandHandler : ICommandHandler<Command.ApproveVolunteerApplication>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _accountRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public ApproveVolunteerApplicationCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _accountRepository = accountRepository;
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.ApproveVolunteerApplication request, CancellationToken cancellationToken)
        {
            //change status application
            var existVolunteerApplication = await _volunteerApplicationDetailRepository.FindByIdAsync(request.detailId);
            existVolunteerApplication.UpdateVolunteerApplication(VolunteerApplicationStatus.Approved, null);
            await _efUnitOfWork.SaveChangesAsync();

            //get account by accountId
            var account = await _accountRepository.FindByIdAsync(existVolunteerApplication.AccountId);

            // Send email
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.ApproveSendMail(Guid.NewGuid(), account.Email, existVolunteerApplication.EventActivity.Name), cancellationToken)
           );
            return Result.Success(new Success(MessagesList.ApproveVolunteerApplicationSuccessfully.GetMessage().Code, MessagesList.ApproveVolunteerApplicationSuccessfully.GetMessage().Message));
        }
    }
}
