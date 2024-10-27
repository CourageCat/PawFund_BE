using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Commands.Message;

public sealed class CreateMessageWithChatBotCommandHandler : ICommandHandler<Command.CreateMessageWithChatBoxCommand, Success<CreateMessageDto>>
{
    private readonly IDialogflowService _dialogflowService;

    public CreateMessageWithChatBotCommandHandler(IDialogflowService dialogService)
    {
        _dialogflowService = dialogService;
    }
    
    public async Task<Result<Success<CreateMessageDto>>> Handle(Command.CreateMessageWithChatBoxCommand request, CancellationToken cancellationToken)
    {
        string sessionId = "your-session-id";
        string languageCode = "vi";
        var textResponse =  await _dialogflowService.DetectIntentAsync(sessionId, request.Content, languageCode);
        Console.WriteLine(textResponse);
        //var result = new CreateMessageDto
        //{
        //    ConnectionStaff = "123",
        //    ConnectionUser = request.UserId,
        //};
        //return Result.Success(new Success<CreateMessageDto>("", "", new ));
        throw new Exception("123");
    }
}
