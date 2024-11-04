using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class ChangePasswordCommandHandler : ICommandHandler<Command.ChangePasswordCommand, Success>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IPublisher _publisher;

    public ChangePasswordCommandHandler
        (IResponseCacheService responseCacheService,
        IPasswordHashService passwordHashService,
        IPublisher publisher,
        IEFUnitOfWork efUnitOfWork)
    {
        _responseCacheService = responseCacheService;
        _passwordHashService = passwordHashService;
        _publisher = publisher;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result<Success>> Handle(Command.ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _efUnitOfWork.AccountRepository.FindByIdAsync(request.UserId);
        if (result.LoginType != Contract.Enumarations.Authentication.LoginType.Local)
            throw new AccountNotLoginLocalException();

        var changePassword = _passwordHashService.HashPassword(request.Password);

        await _responseCacheService.SetCacheResponseAsync($"changepassword_{request.UserId}", changePassword, TimeSpan.FromMinutes(30));

        await Task.WhenAll(
           _publisher.Publish(new DomainEvent.UserPasswordChanged(Guid.NewGuid(), request.UserId, result.Email), cancellationToken)
       );
        
        return Result.Success(new Success(
            MessagesList.AccountChangePasswordSuccess.GetMessage().Code,
            MessagesList.AccountChangePasswordSuccess.GetMessage().Message));
    }
}
