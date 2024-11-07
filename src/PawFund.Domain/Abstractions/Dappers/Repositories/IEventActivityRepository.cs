using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;
using static PawFund.Contract.Services.EventActivity.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IEventActivityRepository : IGenericRepository<EventActivity>
{
    public Task<PagedResult<EventActivity>> GetAllByEventId(Guid id, int pageIndex, int pageSize, EventActivityFilter filterParams, string[] selectedColumns);

    public Task<IEnumerable<EventActivity>> GetApprovedEventsActivityId(Guid id);

}

