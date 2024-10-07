using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Application.UseCases.V1.Commands.Cat;
    public sealed class DeleteCatCommandHandler : ICommandHandler<Command.DeleteCatCommand> 
    {
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public DeleteCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork)
        {
            _catRepository = catRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catFound = await _catRepository.FindByIdAsync(request.Id);
            if (catFound == null || catFound.IsDeleted == true)
            {
                throw new CatException.CatNotFoundException(request.Id);
            }
            catFound.UpdateCat(catFound.Sex, catFound.Name, catFound.Age, catFound.Breed, catFound.Size, catFound.Color, catFound.Description, catFound.BranchId, catFound.CreatedDate, DateTime.Now, true);
            _catRepository.Update(catFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Delete Pet successfully.");
        }
    }

