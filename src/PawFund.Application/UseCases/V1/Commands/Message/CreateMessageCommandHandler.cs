using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Commands.Message;

public sealed class CreateMessageCommandHandler : ICommandHandler<Command.CreateMesssageWithStaffCommand, Success<CreateMessageDto>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IConfiguration _configuration;
    public CreateMessageCommandHandler
        (IDPUnitOfWork dpUnitOfWork,
        IConfiguration configuration)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _configuration = configuration;
    }

    public async Task<Result<Success<CreateMessageDto>>> Handle(Command.CreateMesssageWithStaffCommand request, CancellationToken cancellationToken)
    {
        var staffAssistant = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(_configuration["AccountStaffAssistant:Email"]);

        var result = new CreateMessageDto
        {
            ConnectionStaff = staffAssistant.Id,
            ConnectionUser = request.UserId,
        };
        return Result.Success(new Success<CreateMessageDto>("", "", result));
    }
}
