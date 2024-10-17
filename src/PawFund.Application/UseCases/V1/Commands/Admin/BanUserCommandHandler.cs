using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Admin;
using PawFund.Contract.Services.Admins;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;


namespace PawFund.Application.UseCases.V1.Commands.Admin
{
    public sealed class BanUserCommandHandler : ICommandHandler<Command.BanUserCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _adminRepository;
        private readonly IEFUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public BanUserCommandHandler(IRepositoryBase<Domain.Entities.Account, Guid> adminRepository, IEFUnitOfWork unitOfWork, IPublisher publisher)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _adminRepository.FindByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (user.Status)
            {
                throw new UserException.UserHasAlreadyBannedException();
            }

            user.Status = true;
            _adminRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            //Send Ban Mail
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.UserBan(Guid.NewGuid(), user.Email, request.Reason), cancellationToken)
           );

            return Result.Success("Ban User Successfully");
        }
    }
}
