using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class ChatHistoryRepository(ApplicationDbContext context) : RepositoryBase<ChatHistory, Guid>(context), IChatHistoryRepository
{
}
