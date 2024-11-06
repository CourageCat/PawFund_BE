using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Application.UseCases.V1.Commands.Donate
{
    public sealed class CreateDonationCashCommand : ICommandHandler<Command.CreateDonateCash>
    {
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IRepositoryBase<Donation, Guid> _donationRepository;
        private readonly Domain.Abstractions.Dappers.Repositories.IAccountRepository _accountRepository;

        public CreateDonationCashCommand(IEFUnitOfWork efUnitOfWork, IRepositoryBase<Donation, Guid> donationRepository, Domain.Abstractions.Dappers.Repositories.IAccountRepository accountRepository)
        {
            _efUnitOfWork = efUnitOfWork;
            _donationRepository = donationRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Result> Handle(Command.CreateDonateCash request, CancellationToken cancellationToken)
        {

            var user = await _accountRepository.GetByEmailAsync(request.Email);


            var donation = Donation.CreateDonationCash(request.amount, user.Id, null);
            _donationRepository.Add(donation);
            await _efUnitOfWork.SaveChangesAsync();

            return Result.Success(new Success(MessagesList.CreateDonateCashSuccess.GetMessage().Code, MessagesList.CreateDonateCashSuccess.GetMessage().Message));
        }
    }
}
