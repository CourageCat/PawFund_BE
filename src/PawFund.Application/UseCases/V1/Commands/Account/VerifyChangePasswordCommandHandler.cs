using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class VerifyChangePasswordCommandHandler : ICommandHandler<Command.VerifyChangePasswordCommand, Success>
{
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IResponseCacheService _responseCacheService;

    public VerifyChangePasswordCommandHandler
        (IEFUnitOfWork efUnitOfWork,
        IResponseCacheService responseCacheService)
    {
        _efUnitOfWork = efUnitOfWork;
        _responseCacheService = responseCacheService;
    }

    public async Task<Result<Success>> Handle(Command.VerifyChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _efUnitOfWork.AccountRepository.FindByIdAsync(request.UserId);
        if (user == null) throw new AccountNotFoundException();
        var changePasswordMemory = await _responseCacheService.GetCacheResponseAsync($"changepassword_{request.UserId}");
        var newPassword = JsonConvert.DeserializeObject<string>(changePasswordMemory);

        user.UpdatePassword(newPassword);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        
        await _responseCacheService.DeleteCacheResponseAsync($"changepassword_{request.UserId}");

        return Result.Success(new Success(MessagesList.ChangePasswordSuccess.GetMessage().Code,
            MessagesList.ChangePasswordSuccess.GetMessage().Message
            ));
    }
}
