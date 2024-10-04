using Microsoft.EntityFrameworkCore;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Adopt;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Application.UseCases.V1.Commands.Adopt;
public sealed class CreateAdoptApplicationCommandHandler : ICommandHandler<Command.CreateAdoptApplicationCommand>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptApplicationRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;

    public CreateAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptApplicationRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
    {
        _adoptApplicationRepository = adoptApplicationRepository;
        _efUnitOfWork = efUnitOfWork;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<Result> Handle(Command.CreateAdoptApplicationCommand request, CancellationToken cancellationToken)
    {
        var userFound = await _dbUnitOfWork.AccountRepositories.GetByIdAsync(request.AccountId);
        if (userFound == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(request.AccountId);
        }
        var catFound = await _dbUnitOfWork.CatRepositories.GetByIdAsync(request.CatId);
        if(catFound == null)
        {
            throw new CatException.CatNotFoundException(request.CatId);
        }

        var adoptApplication = new AdoptPetApplication()
        {
            Status = 0,
            IsFinalized = false,
            Description = request.Description,
            AccountId = request.AccountId,  // Set the reference to the existing Account
            CatId = request.CatId,        // Set the reference to the existing Cat
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
        };

        _adoptApplicationRepository.Add(adoptApplication);

        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Create Adopt Pet Application Successfully.");
    }
}

