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
using PawFund.Contract.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Branch
{
    public sealed class CreateBranchCommandHandler : ICommandHandler<Command.CreateBranchCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IConfiguration _configuration;

        public CreateBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IEFUnitOfWork efUnitOfWork, IPasswordHashService passwordHashService, IConfiguration configuration)
        {
            _branchRepository = branchRepository;
            _accountRepository = accountRepository;
            _efUnitOfWork = efUnitOfWork;
            _passwordHashService = passwordHashService;
            _configuration = configuration;
        }

        public async Task<Result> Handle(Command.CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var staffAccountCreated = Domain.Entities.Account.CreateStaffAccount(_passwordHashService.HashPassword(_configuration["PasswordStaff"]), request.Name);
            _accountRepository.Add(staffAccountCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);

            var branchCreated = Domain.Entities.Branch.CreateBranch(request.Name, request.PhoneNumberOfBranch, request.EmailOfBranch, request.Description, request.NumberHome, request.StreetName, request.Ward, request.District, request.Province, request.PostalCode, staffAccountCreated.Id, DateTime.Now, DateTime.Now, false);
            _branchRepository.Add(branchCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.BranchCreateBranchSuccess.GetMessage().Code, MessagesList.BranchCreateBranchSuccess.GetMessage().Message));

        }
    }
}
