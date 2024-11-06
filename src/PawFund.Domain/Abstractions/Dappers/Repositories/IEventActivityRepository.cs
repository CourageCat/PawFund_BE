using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IEventActivityRepository : IGenericRepository<EventActivity>
{
    public Task<IEnumerable<EventActivity>> GetAllByEventId(Guid id);

    public Task<IEnumerable<EventActivity>> GetApprovedEventsActivityId(Guid id);

}

