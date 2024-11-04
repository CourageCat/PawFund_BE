using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Admins;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Shared;
using PawFund.Contract.Services.Admin;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Admin
{
    public sealed class UnBanUserCommandHandler : ICommandHandler<Command.UnBanUserCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _adminRepository;
        private readonly IEFUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public UnBanUserCommandHandler(IRepositoryBase<Domain.Entities.Account, Guid> adminRepository, IEFUnitOfWork unitOfWork, IPublisher publisher)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.UnBanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _adminRepository.FindByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (!user.Status)
            {
                throw new UserException.UserHasAlreadyUnbannedException();
            }

            user.Status = false;
            _adminRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            //Send UnBan Mail
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.UserUnBan(Guid.NewGuid(), user.Email), cancellationToken)
           );


            return Result.Success("UnBan User Successfully");
        }
    }
}
