using MediatR;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System.Security.Cryptography;
using System.Text.Json;
using static PawFund.Domain.Exceptions.AuthenticationException;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class ForgotPasswordEmailCommandHandler : ICommandHandler<Command.ForgotPasswordEmailCommand>
{
    private readonly IDPUnitOfWork _dbUnitOfWork;
    private readonly IPublisher _publisher;
    private readonly IResponseCacheService _responseCacheService;

    public ForgotPasswordEmailCommandHandler
        (IDPUnitOfWork dbUnitOfWork,
        IPublisher publisher,
        IResponseCacheService responseCacheService)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _publisher = publisher;
        _responseCacheService = responseCacheService;
    }
    /// <summary>
    /// Send email to create otp
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="EmailNotFoundException"></exception>

    public async Task<Result> Handle(Command.ForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        // Check email is in system
        var userInfo = await _dbUnitOfWork.AccountRepositories.GetByEmailAsync(request.Email);
        // If user haven't system => Exception
        if (userInfo != null) throw new EmailNotFoundException();
        // If user have type login != Local => Exception
        if (userInfo.LoginType != LoginType.Local)
            throw new EmailGoogleRegistedException();

        // Random OTP
        string otp = GenerateSecureOTP();

        // Save memory
        await _responseCacheService.SetCacheResponseAsync
            ($"forgotpassword_{request.Email}",
            JsonSerializer.Serialize(otp),
            TimeSpan.FromMinutes(15));

        // Send mail notification send otp
        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.UserOtpChanged(Guid.NewGuid(), request.Email, otp), cancellationToken)
        );
        
        return Result.Success(new Success<string>(MessagesList.AuthForgotPasswordEmailSuccess.GetMessage().Code,
            MessagesList.AuthForgotPasswordEmailSuccess.GetMessage().Message, request.Email));
    }

    private static string GenerateSecureOTP()
    {
        var bytes = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        int otp = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 90000 + 10000;
        return otp.ToString(); 
    }
}
