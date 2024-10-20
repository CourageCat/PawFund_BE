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
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;

public sealed class CompleteAdoptionCommandHandler : ICommandHandler<Command.CompleteAdoptionCommand>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptPetApplicationRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IPublisher _publisher;
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public CompleteAdoptionCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptPetApplicationRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher, IDPUnitOfWork dpUnitOfWork)
    {
        _adoptPetApplicationRepository = adoptPetApplicationRepository;
        _efUnitOfWork = efUnitOfWork;
        _publisher = publisher;
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result> Handle(Command.CompleteAdoptionCommand request, CancellationToken cancellationToken)
    {
        //Check Application found
        var applicationFound = await _adoptPetApplicationRepository.FindByIdAsync(request.AdoptId);
        if (applicationFound == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
        }
        //Check Application has already approved (Only Approved Status of Adopt Application can be Completed or Rejected Outside)
        if (applicationFound.Status != AdoptPetApplicationStatus.Approved)
        {
            throw new AdoptApplicationException.AdoptApplicationHasAlreadyCompletedOutSideException();
        }
        //Update Status for Application
        applicationFound.Status = AdoptPetApplicationStatus.ApprovedAndCompleted;
        _adoptPetApplicationRepository.Update(applicationFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        //Send email to Adopter Completed
        await Task.WhenAll(
           _publisher.Publish(new DomainEvent.AdoptionHasBeenCompleted(request.AdoptId, applicationFound.Account.Email, applicationFound.Cat.Name), cancellationToken)
       );

        //Find other applications
        var otherApplications = await _dpUnitOfWork.AdoptRepositories.GetAllApplicationsByCatId(applicationFound.Id, applicationFound.CatId);
        foreach (var application in otherApplications)
        {
            var applicationRejectedFound = await _adoptPetApplicationRepository.FindByIdAsync(application.Id);
            applicationRejectedFound.Status = AdoptPetApplicationStatus.Rejected;
            applicationRejectedFound.ReasonReject = "The Cat has been adopted by another adopter";
            //Update Status and ReasonReject
            _adoptPetApplicationRepository.Update(applicationRejectedFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Send mail Rejected
            await Task.WhenAll(
           _publisher.Publish(new DomainEvent.AdoptionHasBeenRejected(request.AdoptId, application.Account.Email, application.Cat.Name, "The Cat has been adopted by another adopter"), cancellationToken));
        }


        //Return result
        return Result.Success(new Success(MessagesList.AdoptCompleteAdoptApplicationSuccess.GetMessage().Code, MessagesList.AdoptCompleteAdoptApplicationSuccess.GetMessage().Message));
    }
}
