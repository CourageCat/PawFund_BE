using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Admin;
using PawFund.Contract.Services.Admins;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using PawFund.Persistence;


namespace PawFund.Application.UseCases.V1.Commands.Admin
{
    public sealed class BanUserCommandHandler : ICommandHandler<Command.BanUserCommand>
    {
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public BanUserCommandHandler(IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _efUnitOfWork.AccountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new AccountException.AccountNotFoundException();
            }
            if (user.Status)
            {
                throw new UserException.UserHasAlreadyBannedException();
            }
            user.Status = true;
            _efUnitOfWork.AccountRepository.Update(user);
            await _efUnitOfWork.SaveChangesAsync();
            
            //Send Ban Mail
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.UserBan(Guid.NewGuid(), user.Email, request.Reason), cancellationToken)
           );
            return Result.Success(new Success(MessagesList.BanUserSuccess.GetMessage().Code, MessagesList.BanUserSuccess.GetMessage().Message));
        }
    }
}
