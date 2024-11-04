using Newtonsoft.Json;
using MediatR;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Contract.Enumarations.MessagesList;
using static PawFund.Domain.Exceptions.AuthenticationException;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class VerifyEmailCommandHandler : ICommandHandler<Command.VerifyEmailCommand>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IPublisher _publisher;

    public VerifyEmailCommandHandler
        (IRepositoryBase<Domain.Entities.Account, Guid> accountRepository,
        IResponseCacheService responseCacheService,
        IEFUnitOfWork unitOfWork,
        IDPUnitOfWork dbUnitOfWork,
        IPasswordHashService passwordHashService,
        IPublisher publisher)
    {
        _responseCacheService = responseCacheService;
        _efUnitOfWork = unitOfWork;
        _dbUnitOfWork = dbUnitOfWork;
        _passwordHashService = passwordHashService;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var isCheckEmail = await _dbUnitOfWork.AccountRepositories.EmailExistSystemAsync(request.Email);
        // Check user have exit in system by email
        if (isCheckEmail) throw new EmailExistException();

        // Get user registerd in memory
        var registerMemory = await _responseCacheService.GetCacheResponseAsync($"register_{request.Email}");

        // If get with value = null => exception
        if (registerMemory == null) throw new UserNotFoundException();

        string unescapedJson = JsonConvert.DeserializeObject<string>(registerMemory);
        var user = JsonConvert.DeserializeObject<Command.RegisterCommand>(unescapedJson);

        // Hash password
        var passwordHash = _passwordHashService.HashPassword(user.Password);

        // Create object account with type register local
        var accountMember = Domain.Entities.Account.CreateMemberAccountLocal
            (user.FirstName, user.LastName, user.Email, user.PhoneNumber, passwordHash, user.Gender);

        _efUnitOfWork.AccountRepository.Add(accountMember);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        // Delete object saved in memory
        await _responseCacheService.DeleteCacheResponseAsync($"register_{request.Email}");

        // Send email when verified successfully
        await Task.WhenAll(
                _publisher.Publish(new DomainEvent.UserVerifiedEmailRegist(Guid.NewGuid(), request.Email),
                cancellationToken)
        );

        return Result.Success(new Success(MessagesList.VerifyEmailSuccess.GetMessage().Code,
            MessagesList.VerifyEmailSuccess.GetMessage().Message));
    }
}
