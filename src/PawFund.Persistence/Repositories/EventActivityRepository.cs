using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class EventActivityRepository(ApplicationDbContext context) : RepositoryBase<EventActivity, Guid>(context), IEventActivityRepository
{
}