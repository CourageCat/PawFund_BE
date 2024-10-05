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
using PawFund.Contract.Services.Adopt;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Adopt;

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
        if (request.CatId != null) {
            var catFound = await _dbUnitOfWork.CatRepositories.GetByIdAsync((Guid)request.CatId);
            if (catFound == null)
            {
                throw new CatException.CatNotFoundException((Guid)request.CatId);
            }
        }
        var adoptApplicationFound = await _dbUnitOfWork.AdoptRepositories.GetByIdAsync(request.AdoptId);
        if(adoptApplicationFound == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
        }
        //adoptApplicationFound.Description = request.Description;
        //adoptApplicationFound.CatId = (Guid)request.CatId;
        //adoptApplicationFound.ModifiedDate = DateTime.UtcNow;
        var adoptApplicationUpdated = new AdoptPetApplication()
        {
            Id = adoptApplicationFound.Id,
            Description = request.Description,
            Status = adoptApplicationFound.Status,
            IsFinalized = adoptApplicationFound.IsFinalized,
            AccountId = adoptApplicationFound.Account.Id,
            CatId = (Guid)(request.CatId != null ? request.CatId : adoptApplicationFound.Cat.Id),
            CreatedDate = adoptApplicationFound.CreatedDate,
            ModifiedDate = DateTime.Now,
            IsDeleted = false
        };
        _adoptApplicationRepository.Update(adoptApplicationUpdated);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Update Adopt Pet Application Successfully.");
    }
}

