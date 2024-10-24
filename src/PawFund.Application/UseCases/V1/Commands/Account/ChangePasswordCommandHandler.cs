using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class ChangePasswordCommandHandler : ICommandHandler<Command.ChangePasswordCommand, Success>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IPublisher _publisher;

    public ChangePasswordCommandHandler
        (IResponseCacheService responseCacheService,
        IDPUnitOfWork dpUnitOfWork,
        IPasswordHashService passwordHashService,
        IPublisher publisher)
    {
        _responseCacheService = responseCacheService;
        _dpUnitOfWork = dpUnitOfWork;
        _passwordHashService = passwordHashService;
        _publisher = publisher;
    }

    public async Task<Result<Success>> Handle(Command.ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _dpUnitOfWork.AccountRepositories.GetByIdAsync(request.UserId);
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
