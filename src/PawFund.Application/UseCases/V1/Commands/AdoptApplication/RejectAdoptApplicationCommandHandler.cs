using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.AdoptPetApplication;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication
{
    public class RejectAdoptApplicationCommandHandler : ICommandHandler<Command.RejectAdoptApplicationCommand>
    {
        public readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptPetApplicationRepository;
        public readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public RejectAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptPetApplicationRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _adoptPetApplicationRepository = adoptPetApplicationRepository;
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.RejectAdoptApplicationCommand request, CancellationToken cancellationToken)
        {
            //Check Application found
            var applicationFound = await _adoptPetApplicationRepository.FindByIdAsync(request.AdoptId);
            if (applicationFound == null)
            {
                throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
            }
            //Check Application has already approved (Only Pending Status of Adopt Application can be rejected)
            if (applicationFound.Status != AdoptPetApplicationStatus.Pending)
            {
                throw new AdoptApplicationException.AdoptApplicationHasAlreadyRejectedException();
            }
            //Update Status for Application
            applicationFound.Status = AdoptPetApplicationStatus.Rejected;
            applicationFound.ReasonReject = request.ReasonReject;
            _adoptPetApplicationRepository.Update(applicationFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Send email
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.AdoptionHasBeenRejected(request.AdoptId, applicationFound.Account.Email, applicationFound.Cat.Name, request.ReasonReject), cancellationToken)
           );

            //Return result
            return Result.Success(new Success(MessagesList.AdoptRejectApplicationSuccess.GetMessage().Code, MessagesList.AdoptRejectApplicationSuccess.GetMessage().Message));

        }
    }
}
