using MediatR;
using PawFund.Contract.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System.Text.Json;
using static PawFund.Domain.Exceptions.AuthenticationException;

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
        var isCheckEmail = await _dbUnitOfWork.AccountRepositories.EmailExistSystem(request.Email);
        if(isCheckEmail) throw new EmailExistException();

        // Save memory
        await _responseCacheService.SetCacheResponseAsync
            ($"register_{request.Email}", 
            JsonSerializer.Serialize(request), 
            TimeSpan.FromHours(12));

        // Send mail
        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.UserCreated(Guid.NewGuid(), request.Email), cancellationToken)
        );

        return Result.Success("Registration successful, please check email for confirmation");
    }
}
