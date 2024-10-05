using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Contract.Services.Adopt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Adopt
{
    public sealed class DeleteAdoptApplicationByAdopterCommandHandler : ICommandHandler<Command.DeleteAdoptApplicationByAdopterCommand>
    {
        private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptApplicationRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dbUnitOfWork;

        public DeleteAdoptApplicationByAdopterCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptApplicationRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
        {
            _adoptApplicationRepository = adoptApplicationRepository;
            _efUnitOfWork = efUnitOfWork;
            _dbUnitOfWork = dbUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteAdoptApplicationByAdopterCommand request, CancellationToken cancellationToken)
        {
            var adoptApplicationFound = await _dbUnitOfWork.AdoptRepositories.GetByIdAsync(request.AdoptId);
            if(adoptApplicationFound == null)
            {
                throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
            }
            _adoptApplicationRepository.Remove(adoptApplicationFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Delete Adopt Pet Application Successfully.");
        }
    }
}
