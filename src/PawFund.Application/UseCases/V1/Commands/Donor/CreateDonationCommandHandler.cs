using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.Donors;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;


namespace PawFund.Application.UseCases.V1.Commands.Donor
{
    public sealed class CreateDonationCommandHandler : ICommandHandler<Command.CreateDonationCommand>
    {
        private readonly IRepositoryBase<Donation, Guid> _donationRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dbUnitOfWork;

        public CreateDonationCommandHandler(IRepositoryBase<Donation, Guid> donationRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
        {
            _donationRepository = donationRepository;
            _efUnitOfWork = efUnitOfWork;
            _dbUnitOfWork = dbUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var donation = new Donation()
            {
               
                Amount = request.amount,
                Description = request.description,
                AccountId = 
                PaymentMethodId = request.PaymentMethodId,
                Status = false

            };

            _donationRepository.Add(donation); // Lưu donation vào repository

            await _efUnitOfWork.SaveChangesAsync(cancellationToken); // Lưu thay đổi vào DB
            return Result.Success("Donation created successfully.");
        }
    }
}
