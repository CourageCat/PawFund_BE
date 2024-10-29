using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Contract.DTOs.MessageDTOs;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;

namespace PawFund.Application.UseCases.V1.Commands.Message;

public sealed class CreateMessageWithUserCommandHandler : ICommandHandler<Command.CreateMesssageWithUserCommand, Success<CreateMessageDto>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly AccountStaffAssistantSetting _staffAssistantSetting;
    private readonly IRepositoryBase<Domain.Entities.ChatHistory, Guid> _chatHistoryRepository;
    private readonly IRepositoryBase<Domain.Entities.Message, Guid> _messageRepository;

    public CreateMessageWithUserCommandHandler
        (IDPUnitOfWork dpUnitOfWork,
        IOptions<AccountStaffAssistantSetting> staffAssistantConfig,
        IEFUnitOfWork efUnitOfWork,
        IRepositoryBase<Domain.Entities.ChatHistory, Guid> chatHistoryRepository,
        IRepositoryBase<Domain.Entities.Message, Guid> messageRepository)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _efUnitOfWork = efUnitOfWork;
        _staffAssistantSetting = staffAssistantConfig.Value;
        _chatHistoryRepository = chatHistoryRepository;
        _messageRepository = messageRepository;
    }

    public async Task<Result<Success<CreateMessageDto>>> Handle(Command.CreateMesssageWithUserCommand request, CancellationToken cancellationToken)
    {
        var bot = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(_staffAssistantSetting.Email);

        var result = new CreateMessageDto
        {
            SenderId = bot.Id,
            ReceiverId = request.UserId,
            Content = request.Content,
        };

        var messageEntity = new Domain.Entities.Message
        {
            SenderId = result.SenderId,
            ReceiverId = result.ReceiverId,
            Content = result.Content,
        };

        _messageRepository.Add(messageEntity);
        await UpdateChatHistory(request.UserId, bot.Id, true, request.Content);
        await UpdateChatHistory(bot.Id, request.UserId, true, request.Content);
        await _efUnitOfWork.SaveChangesAsync();

        return Result.Success(new Success<CreateMessageDto>("", "", result));
    }

    public async Task UpdateChatHistory(Guid senderId, Guid receiverId, bool read, string content)
    {
        var chatHistory = await _dpUnitOfWork.ChatHistoryRepository.GetChatSenderAndRecieverHistoryAsync(new CreateChatHistoryDTO
        {
            UserId = senderId,
            ChatPartnerId = receiverId,
            Read = read,
        });
        if (chatHistory == null)
        {
            ChatHistory createChatHistory = ChatHistory.CreateChatHistory(Guid.NewGuid(), senderId, receiverId, read, content);
            await _dpUnitOfWork.ChatHistoryRepository.AddAsync(createChatHistory);
            return;
        }
        ChatHistory updateChatHistory = ChatHistory.UpdateChatHistory(chatHistory.Id, chatHistory.UserId, chatHistory.ChatPartnerId, true, content, (DateTime)chatHistory.CreatedDate);
        await _dpUnitOfWork.ChatHistoryRepository.UpdateAsync(updateChatHistory);
        return;
    }
}
