using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Contract.Services.AdoptApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication
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
            //Check Adopt Application found
            var adoptApplicationFound = await _adoptApplicationRepository.FindByIdAsync(request.AdoptId);
            if (adoptApplicationFound == null)
            {
                throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
            }
            //Remove Adopt Application
            _adoptApplicationRepository.Remove(adoptApplicationFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Return result
            return Result.Success(new Success(MessagesList.AdoptDeleteApplicationSuccess.GetMessage().Code, MessagesList.AdoptDeleteApplicationSuccess.GetMessage().Message));
        }
    }
}
