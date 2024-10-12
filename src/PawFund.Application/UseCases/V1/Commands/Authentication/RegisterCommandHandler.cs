using MediatR;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System.Text.Json;
using static PawFund.Domain.Exceptions.AuthenticationException;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class RegisterCommandHandler : ICommandHandler<Command.RegisterCommand>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IPublisher _publisher;
    private readonly IDPUnitOfWork _dbUnitOfWork;
    public RegisterCommandHandler
        (IResponseCacheService responseCacheService,
        IPublisher publisher,
        IDPUnitOfWork unitOfWork)
    {
        _responseCacheService = responseCacheService;
        _publisher = publisher;
        _dbUnitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbUnitOfWork.AccountRepositories.EmailExistSystem(request.Email);
        // Check user have exits in system, if exit not regist
        if (user) throw new EmailExistException();

        // Save memory in 12 hour
        await _responseCacheService.SetCacheResponseAsync
            ($"register_{request.Email}",
            JsonSerializer.Serialize(request),
            TimeSpan.FromHours(12));

        // Send mail to notification user created, and wait user accept
        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.UserCreated(Guid.NewGuid(), request.Email), cancellationToken)
        );

        return Result.Success(new Success(MessagesList.AuthRegisterSuccess.GetMessage().Code,
            MessagesList.AuthRegisterSuccess.GetMessage().Message));
    }
}
