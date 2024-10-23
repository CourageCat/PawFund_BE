using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Cat;
public sealed class UpdateCatCommandHandler : ICommandHandler<Command.UpdateCatCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;

    public UpdateCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork)
    {
        _catRepository = catRepository;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateCatCommand request, CancellationToken cancellationToken)
    {
        var catFound = await _catRepository.FindByIdAsync(request.Id);
        if (catFound == null || catFound.IsDeleted == true)
        {
            throw new CatException.CatNotFoundException(request.Id);
        }
        catFound.UpdateCat(request.Sex, request.Name, request.Age, request.Breed, request.Size, request.Color, request.Description);
        _catRepository.Update(catFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Update Pet successfully.");
    }
}

