using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;

public sealed class UpdateAdoptApplicationCommandHandler : ICommandHandler<Command.UpdateAdoptApplicationCommand>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptApplicationRepository;
    private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
    private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;

    public UpdateAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptApplicationRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
    {
        _adoptApplicationRepository = adoptApplicationRepository;
        _accountRepository = accountRepository;
        _catRepository = catRepository;
        _efUnitOfWork = efUnitOfWork;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateAdoptApplicationCommand request, CancellationToken cancellationToken)
    {
        //Check Adopt Application found
        var adoptApplicationFound = await _adoptApplicationRepository.FindByIdAsync(request.AdoptId);
        if (adoptApplicationFound == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
        }
        //Check if User want to Update cat
        if (request.CatId != null)
        {
            //Check Cat found
            var catFound = await _catRepository.FindByIdAsync((Guid)request.CatId);
            if (catFound == null)
            {
                throw new CatException.CatNotFoundException();
            }
            //Check Account has already register with Cat
            var hasAccountRegisteredWithCat = await _dbUnitOfWork.AdoptRepositories.HasAccountRegisterdWithPetAsync(adoptApplicationFound.AccountId, (Guid)request.CatId);
            if (hasAccountRegisteredWithCat)
            {
                throw new AdoptApplicationException.AdopterHasAlreadyRegisteredWithCatException();
            }
        }

        //Update Adopt Pet Application
        var catId = (Guid)(request.CatId != null ? request.CatId : adoptApplicationFound.Cat.Id);
        adoptApplicationFound.UpdateAdoptPetApplication(adoptApplicationFound.MeetingDate, adoptApplicationFound.ReasonReject, adoptApplicationFound.Status, adoptApplicationFound.IsFinalized, request.Description, adoptApplicationFound.AccountId, catId, adoptApplicationFound.CreatedDate, adoptApplicationFound.ModifiedDate, adoptApplicationFound.IsDeleted);

        _adoptApplicationRepository.Update(adoptApplicationFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        //Return result
        return Result.Success(new Success(MessagesList.AdoptUpdateApplicationSuccess.GetMessage().Code, MessagesList.AdoptUpdateApplicationSuccess.GetMessage().Message));
    }
}

