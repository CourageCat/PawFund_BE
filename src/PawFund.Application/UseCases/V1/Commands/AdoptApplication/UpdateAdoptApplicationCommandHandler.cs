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
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;

    public UpdateAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptApplicationRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
    {
        _adoptApplicationRepository = adoptApplicationRepository;
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
            var catFound = await _dbUnitOfWork.CatRepositories.GetByIdAsync((Guid)request.CatId);
            if (catFound == null)
            {
                throw new CatException.CatNotFoundException((Guid)request.CatId);
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

