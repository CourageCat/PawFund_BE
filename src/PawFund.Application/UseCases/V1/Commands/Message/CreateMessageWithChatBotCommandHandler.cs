using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.MessageDTOs;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Commands.Message;

public sealed class CreateMessageWithChatBotCommandHandler : ICommandHandler<Command.CreateMessageWithChatBoxCommand, Success<CreateMessageDto>>
{
    private readonly IDialogflowService _dialogflowService;
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly AccountStaffBotSetting _staffBotSetting;
    public CreateMessageWithChatBotCommandHandler
        (IDialogflowService dialogService,
        IDPUnitOfWork dpUnitOfWork,
        IOptions<AccountStaffBotSetting> staffBotConfig)
    {
        _dialogflowService = dialogService;
        _dpUnitOfWork = dpUnitOfWork;
        _staffBotSetting = staffBotConfig.Value;
    }
    
    public async Task<Result<Success<CreateMessageDto>>> Handle(Command.CreateMessageWithChatBoxCommand request, CancellationToken cancellationToken)
    {
        var bot = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(_staffBotSetting.Email);
        
        string sessionId = "your-session-id";
        string languageCode = "vi";
        var textResponse =  await _dialogflowService.DetectIntentAsync(sessionId, request.Content, languageCode);
        var result = new CreateMessageDto
        {
            SenderId = bot.Id,
            ReceiverId = request.UserId,
            Content = textResponse,
        };

        return Result.Success(new Success<CreateMessageDto>("", "", result));
    }
}
