using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;

namespace PawFund.Contract.Services.Messages;

public static class Command
{
    public record CreateMesssageWithStaffCommand(Guid UserId, string Content) : ICommand<Success<CreateMessageDto>>;
    public record CreateMessageWithChatBoxCommand(Guid UserId, string Content) : ICommand<Success<CreateMessageDto>>;
}
