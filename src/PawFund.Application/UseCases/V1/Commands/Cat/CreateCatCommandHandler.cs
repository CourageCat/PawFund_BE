using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Cat
{
    public sealed class CreateCatCommandHandler : ICommandHandler<Command.CreateCatCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public CreateCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork)
        {
            _catRepository = catRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateCatCommand request, CancellationToken cancellationToken)
        {
            //var branchFound = await _branchRepository.FindByIdAsync(request.BranchId);
            ////if(branchFound == null)
            ////{
            ////    throw new BranchException.BranchNotFoundException(request.BranchId);
            ////}
            var catCreated = Domain.Entities.Cat.CreateCat(request.Sex, request.Name, request.Age, request.Breed, request.Size, request.Color, request.Description, request.BranchId, DateTime.Now, DateTime.Now, false);
            _catRepository.Add(catCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Create Cat Successfully.");

        }
    }
}
