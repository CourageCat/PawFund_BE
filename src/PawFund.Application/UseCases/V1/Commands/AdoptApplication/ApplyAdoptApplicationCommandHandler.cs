using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.AdoptPetApplication;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.DTOs.Adopt.Response;
using static PawFund.Domain.Exceptions.AdoptApplicationException;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;

public sealed class ApplyAdoptApplicationCommandHandler : ICommandHandler<Command.ApplyAdoptApplicationCommand>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptPetApplicationRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IPublisher _publisher;
    private readonly IResponseCacheService _responseCacheService;

    public ApplyAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptPetApplicationRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher, IResponseCacheService responseCacheService)
    {
        _adoptPetApplicationRepository = adoptPetApplicationRepository;
        _efUnitOfWork = efUnitOfWork;
        _publisher = publisher;
        _responseCacheService = responseCacheService;
    }

    public async Task<Result> Handle(Command.ApplyAdoptApplicationCommand request, CancellationToken cancellationToken)
    {
        //Check Application found
        var applicationFound = await _adoptPetApplicationRepository.FindByIdAsync(request.AdoptId);
        if (applicationFound == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
        }
        //Check Application has already approved (Only Pending Status of Adopt Application can be approved)
        if (applicationFound.Status != AdoptPetApplicationStatus.Pending)
        {
            throw new AdoptApplicationException.AdoptApplicationHasAlreadyApprovedException();
        }
        //Check if exist any meeting time
        var branchName = applicationFound.Cat.Branch.Name;
        var listMeetingTime = await _responseCacheService.GetListAsync<GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO>(branchName);
        if(listMeetingTime == null)
        {
            throw new NotUpdateMeetingTimeBeforeApplyAdoptApplicationException();
        }
        
        //Update Status for Application
        applicationFound.Status = AdoptPetApplicationStatus.Approved;
        _adoptPetApplicationRepository.Update(applicationFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        //Send email
        await Task.WhenAll(
           _publisher.Publish(new DomainEvent.AdoptionHasBeenApproved(request.AdoptId, applicationFound.Account.Email, applicationFound.Cat.Name), cancellationToken)
       );

        //Return result
        return Result.Success(new Success(MessagesList.AdoptApproveApplicationSuccess.GetMessage().Code, MessagesList.AdoptApproveApplicationSuccess.GetMessage().Message));

    }
}
