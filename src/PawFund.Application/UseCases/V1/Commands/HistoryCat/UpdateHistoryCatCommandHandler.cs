using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.HistoryCats;
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

namespace PawFund.Application.UseCases.V1.Commands.HistoryCat;
public sealed class UpdateHistoryCatCommandHandler : ICommandHandler<Command.UpdateHistoryCatCommand>
{
    private readonly IRepositoryBase<Domain.Entities.HistoryCat, Guid> _historycatRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;

    public UpdateHistoryCatCommandHandler(IRepositoryBase<Domain.Entities.HistoryCat, Guid> historycatRepository, IEFUnitOfWork efUnitOfWork)
    {
        _historycatRepository = historycatRepository;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateHistoryCatCommand request, CancellationToken cancellationToken)
    {
        var historycatFound = await _historycatRepository.FindByIdAsync(request.Id);
        if (historycatFound == null || historycatFound.IsDeleted == true)
        {
            throw new HistoryCatException.HistoryCatNotFoundException(request.Id);
        }
        historycatFound.UpdateHistoryCat(historycatFound.DateAdopt, historycatFound.CatId, historycatFound.AccountId, (DateTime)historycatFound.CreatedDate, DateTime.Now, true);
        _historycatRepository.Update(historycatFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Update History Pet successfully.");
    }
}

