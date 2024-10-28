using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IChatHistoryRepository : IGenericRepository<Domain.Entities.ChatHistory>
{
    Task<ChatHistory> GetChatSenderAndRecieverHistoryAsync(CreateChatHistoryDTO createChatHistoryDto);
    Task<List<ChatHistory>> GetUserNeedSupportAsync(Guid userStaffId);
}