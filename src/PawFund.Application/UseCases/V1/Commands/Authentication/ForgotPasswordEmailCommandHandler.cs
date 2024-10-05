using MediatR;
using PawFund.Contract.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System.Security.Cryptography;
using System.Text.Json;
using static PawFund.Domain.Exceptions.AuthenticationException;

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
        var isCheckEmail = await _dbUnitOfWork.AccountRepositories.EmailExistSystem(request.Email);
        // If email is not in the system, return error
        if (!isCheckEmail) throw new EmailNotFoundException();

        // Random OTP 
        string otp = GenerateSecureOTP();

        // Save memory
        await _responseCacheService.SetCacheResponseAsync
            ($"forgotpassword_{request.Email}",
            JsonSerializer.Serialize(otp),
            TimeSpan.FromMinutes(15));

        // Send mail
        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.UserOtpChanged(Guid.NewGuid(), request.Email, otp), cancellationToken)
        );
        
        return Result.Success("Please check your email to enter otp");
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
