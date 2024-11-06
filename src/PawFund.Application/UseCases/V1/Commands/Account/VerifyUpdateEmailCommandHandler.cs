using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class VerifyUpdateEmailCommandHandler : ICommandHandler<Command.VerifyUpdateEmailCommand, Success>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;

    public VerifyUpdateEmailCommandHandler
        (IResponseCacheService responseCacheService,
        IRepositoryBase<Domain.Entities.Account, Guid> accountRepository,
        IEFUnitOfWork efUnitOfWork)
    {
        _responseCacheService = responseCacheService;
        _accountRepository = accountRepository;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result<Success>> Handle(Command.VerifyUpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var changeEmailMemory = await _responseCacheService.GetCacheResponseAsync($"changeemail_{request.UserId}");
        var newEmail = JsonConvert.DeserializeObject<string>(changeEmailMemory);
        var user = await _accountRepository.FindByIdAsync(request.UserId);
        if (user == null) throw new AccountNotFoundException();
        
        user.UpdateEmail(newEmail);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        await _responseCacheService.DeleteCacheResponseAsync($"changeemail_{request.UserId}");
        
        return Result.Success(new Success(MessagesList.AccountUpdateEmailSuccess.GetMessage().Code,
            MessagesList.AccountUpdateEmailSuccess.GetMessage().Message
            ));
    }
}
