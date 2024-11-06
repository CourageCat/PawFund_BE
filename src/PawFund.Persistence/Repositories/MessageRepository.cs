using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class MessageRepository(ApplicationDbContext context) : RepositoryBase<Message, Guid>(context), IMessageRepository
{
}