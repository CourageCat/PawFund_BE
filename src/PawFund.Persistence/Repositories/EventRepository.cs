using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;
public class EventRepository(ApplicationDbContext context) : RepositoryBase<Event, Guid>(context), IEventRepository
{
}