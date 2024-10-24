using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Branchs;
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
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Branch;
public sealed class UpdateBranchCommandHandler : ICommandHandler<Command.UpdateBranchCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;

    public UpdateBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork)
    {
        _branchRepository = branchRepository;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branchFound = await _branchRepository.FindByIdAsync(request.Id);
        if (branchFound == null || branchFound.IsDeleted == true)
        {
            throw new BranchException.BranchNotFoundException(request.Id);
        }
        branchFound.UpdateBranch(request.Name, request.PhoneNumberOfBranch, request.EmailOfBranch, request.Description, request.NumberHome, request.StreetName, request.Ward, request.District, request.Province, request.PostalCode, branchFound.AccountId, DateTime.Now, DateTime.Now, false);
        _branchRepository.Update(branchFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new Success(MessagesList.BranchUpdateBranchSuccess.GetMessage().Code, MessagesList.BranchUpdateBranchSuccess.GetMessage().Message));
    }
}

