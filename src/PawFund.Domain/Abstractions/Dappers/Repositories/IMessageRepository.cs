using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IMessageRepository : IGenericRepository<Domain.Entities.Message>
{
    Task<List<Message>> GetMessagesChatsAsync(Guid senderId, Guid receiverId);
}
