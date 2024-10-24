using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.HistoryCats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.HistoryCat
{
    public sealed class CreateHistoryCatCommandHandler : ICommandHandler<Command.CreateHistoryCatCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.HistoryCat, Guid> _historycatRepository;
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public CreateHistoryCatCommandHandler(IRepositoryBase<Domain.Entities.HistoryCat, Guid> historycatRepository, IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IEFUnitOfWork efUnitOfWork)
        {
            _historycatRepository = historycatRepository;
            _catRepository = catRepository;
            _accountRepository = accountRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateHistoryCatCommand request, CancellationToken cancellationToken)
        {
            var accountFound = await _accountRepository.FindByIdAsync(request.AccountId);
            if (accountFound == null)
                throw new AccountException.AccountNotFoundException();
            var catFound = await _catRepository.FindByIdAsync(request.CatId);
            if (catFound == null)
                throw new CatException.CatNotFoundException(request.CatId);
            var historycatCreated = Domain.Entities.HistoryCat.CreateHistoryCat(request.DateAdopt, request.CatId, request.AccountId, DateTime.Now, DateTime.Now, false);
            _historycatRepository.Add(historycatCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Create History Cat Successfully.");

        }
    }
}
