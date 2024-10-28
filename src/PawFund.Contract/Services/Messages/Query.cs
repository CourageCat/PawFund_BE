using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Contract.DTOs.MessageDTOs;

namespace PawFund.Contract.Services.Messages;

public static class Query
{
    public record GetListUserNeedSupportQuery() : IQuery<Success<List<ChatHistoryDto>>>;
    public record GetMessagesSenderIdQuery(Guid SenderId) : IQuery<Success<List<CreateMessageDto>>>;
}
