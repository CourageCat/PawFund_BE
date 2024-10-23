using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.Authentication;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class UpdateEmailCommandHandler : ICommandHandler<Command.UpdateEmailCommand, Success>
{
    private readonly IPublisher _publisher;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public UpdateEmailCommandHandler(IPublisher publisher,
        IResponseCacheService responseCacheService,
        IDPUnitOfWork dpUnitOfWork)
    {
        _publisher = publisher;
        _responseCacheService = responseCacheService;
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Success>> Handle(Command.UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var isCheckMail = await _dpUnitOfWork.AccountRepositories.EmailExistSystemAsync(request.Email);
        if (isCheckMail == true)
            throw new AccountUpdateEmailExit();

        var account = await _dpUnitOfWork.AccountRepositories.GetByIdAsync(request.UserId);
        if (account == null)
            throw new AccountEmailDuplicateException();

        if(account.LoginType != LoginType.Local)
            throw new AccountNotLoginLocalException();


        await _responseCacheService.SetCacheResponseAsync($"changeemail_{request.UserId}", request.Email, TimeSpan.FromMinutes(30));
        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.UserEmailChanged(Guid.NewGuid(), request.UserId, request.Email), cancellationToken)
        );
        
        return Result.Success(new Success(MessagesList.AccountUpdateChangeEmail.GetMessage().Code, MessagesList.AccountUpdateChangeEmail.GetMessage().Message));
    }
}
