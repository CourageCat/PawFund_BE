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

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;

public sealed class ApplyAdoptApplicationCommandHandler : ICommandHandler<Command.ApplyAdoptApplicationCommand>
{
    public readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptPetApplicationRepository;
    public readonly IRepositoryBase<Account, Guid> _accountRepository;
    public readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
    public readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IPublisher _publisher;

    public ApplyAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptPetApplicationRepository, IRepositoryBase<Account, Guid> accountRepository, IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
    {
        _adoptPetApplicationRepository = adoptPetApplicationRepository;
        _accountRepository = accountRepository;
        _catRepository = catRepository;
        _efUnitOfWork = efUnitOfWork;
        _publisher = publisher;
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
        //Find Adopter
        var adopterFound = await _accountRepository.FindByIdAsync(applicationFound.AccountId);
        if(adopterFound == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(applicationFound.AccountId);
        }
        var catFound = await _catRepository.FindByIdAsync(applicationFound.CatId);
        if(catFound == null)
        {
            throw new CatException.CatNotFoundException(applicationFound.CatId);
        }
        //Update Status for Application
        applicationFound.Status = AdoptPetApplicationStatus.Approved;
        _adoptPetApplicationRepository.Update(applicationFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        //Send email
        await Task.WhenAll(
           _publisher.Publish(new DomainEvent.AdopterHasBeenApproved(request.AdoptId, adopterFound.Email, catFound.Name), cancellationToken)
       );

        //Return result
        return Result.Success(new Success(MessagesList.AdoptApproveApplicationSuccess.GetMessage().Code, MessagesList.AdoptApproveApplicationSuccess.GetMessage().Message));

    }
}
