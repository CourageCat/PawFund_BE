using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Application.UseCases.V1.Commands.Branch;
    public sealed class DeleteBranchCommandHandler : ICommandHandler<Command.DeleteBranchCommand> 
    {
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public DeleteBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork)
        {
            _branchRepository = branchRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var branchFound = await _branchRepository.FindByIdAsync(request.Id);
            if (branchFound == null || branchFound.IsDeleted == true)
            {
                throw new BranchException.BranchNotFoundException(request.Id);
            }
            branchFound.UpdateBranch(branchFound.Name, branchFound.PhoneNumberOfBranch, branchFound.EmailOfBranch, branchFound.Description, branchFound.NumberHome, branchFound.StreetName, branchFound.Ward, branchFound.District, branchFound.Province, branchFound.PostalCode, branchFound.AccountId, (DateTime)branchFound.CreatedDate, DateTime.Now, false);
            _branchRepository.Update(branchFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Delete Branch successfully.");
        }
    }

