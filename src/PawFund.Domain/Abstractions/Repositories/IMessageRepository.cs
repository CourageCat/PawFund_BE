using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IMessageRepository : IRepositoryBase<Message, Guid>
{
}
