using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Branchs;
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

namespace PawFund.Application.UseCases.V1.Commands.Branch
{
    public sealed class CreateBranchCommandHandler : ICommandHandler<Command.CreateBranchCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public CreateBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork)
        {
            _branchRepository = branchRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branchCreated = Domain.Entities.Branch.CreateBranch(request.Name, request.PhoneNumberOfBranch, request.EmailOfBranch, request.Description, request.NumberHome, request.StreetName, request.Ward, request.District, request.Province, request.PostalCode, DateTime.Now, DateTime.Now, false);
            _branchRepository.Add(branchCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Create Branch Successfully.");

        }
    }
}
