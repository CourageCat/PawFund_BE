using MediatR;
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

public sealed class ForgotPasswordChangeCommandHandler : ICommandHandler<Command.ForgotPasswordChangeCommand>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IRepositoryBase<Account, Guid> _accountRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IPublisher _publisher;
    private readonly IResponseCacheService _responseCacheService;
    public ForgotPasswordChangeCommandHandler
        (IDPUnitOfWork dpUnitOfWork,
        IEFUnitOfWork efUnitOfWork,
        IRepositoryBase<Account, Guid> accountRepository,
        IPasswordHashService passwordHashService,
        IPublisher publisher,
        IResponseCacheService responseCacheService)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _efUnitOfWork = efUnitOfWork;
        _accountRepository = accountRepository;
        _passwordHashService = passwordHashService;
        _publisher = publisher;
        _responseCacheService = responseCacheService;
    }
    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ErrorChangePassword"></exception>
    public async Task<Result> Handle(Command.ForgotPasswordChangeCommand request, CancellationToken cancellationToken)
    {
        // Get otp from previous step
        var forgotPasswordMemory = await _responseCacheService.GetCacheResponseAsync($"passwordchange_{request.Email}");
        string unescapedJson = JsonConvert.DeserializeObject<string>(forgotPasswordMemory);
        var otp = JsonConvert.DeserializeObject<string>(unescapedJson);

        // Check if the otp created from the previous step matches the otp sent by the client
        if (otp != request.Otp) throw new ErrorChangePassword();

        // Update account
        var account = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(request.Email);
        var newPassword = _passwordHashService.HashPassword(request.Password);
        account.Password = newPassword;
        _accountRepository.Update(account);
        await _efUnitOfWork.SaveChangesAsync();

        // Send email
        await Task.WhenAll(
           _publisher.Publish(new DomainEvent.UserPasswordChanged(Guid.NewGuid(), request.Email), cancellationToken)
       );

        await _responseCacheService.DeleteCacheResponseAsync($"passwordchange_{request.Email}");

        return Result.Success("Your account password has been changed successfully.");
    }
}
