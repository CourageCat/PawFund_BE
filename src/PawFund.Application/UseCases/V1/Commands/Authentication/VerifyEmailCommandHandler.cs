using Newtonsoft.Json;
using PawFund.Contract.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using static PawFund.Domain.Exceptions.AuthenticationException;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class VerifyEmailCommandHandler : ICommandHandler<Command.VerifyEmailCommand>
{
    private readonly IRepositoryBase<Account, Guid> _accountRepository;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;
    private readonly IPasswordHashService _passwordHashService;

    public VerifyEmailCommandHandler
        (IRepositoryBase<Account, Guid> accountRepository,
        IResponseCacheService responseCacheService,
        IEFUnitOfWork unitOfWork,
        IDPUnitOfWork dbUnitOfWork,
        IPasswordHashService passwordHashService)
    {
        _accountRepository = accountRepository;
        _responseCacheService = responseCacheService;
        _efUnitOfWork = unitOfWork;
        _dbUnitOfWork = dbUnitOfWork;
        _passwordHashService = passwordHashService;
    }

    public async Task<Result> Handle(Command.VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var isCheckEmail = await _dbUnitOfWork.AccountRepositories.EmailExistSystem(request.Email);
        if (isCheckEmail) throw new EmailExistException();

        var registerMemory = await _responseCacheService.GetCacheResponseAsync($"register_{request.Email}");

        if (registerMemory == null) throw new UserNotFoundException();

        string unescapedJson = JsonConvert.DeserializeObject<string>(registerMemory);
        var user = JsonConvert.DeserializeObject<Command.RegisterCommand>(unescapedJson);

        var passwordHash = _passwordHashService.HashPassword(user.Password);

        var accountMember = Account.CreateMemberAccount
            (user.FirstName, user.LastName, user.Email, user.PhoneNumber, passwordHash);

        _accountRepository.Add(accountMember);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        await _responseCacheService.DeleteCacheResponseAsync($"register_{request.Email}");

        return Result.Success("Account confirmation successful");
    }
}
