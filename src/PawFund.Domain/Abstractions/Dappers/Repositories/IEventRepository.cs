using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Event;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Event.Filter;
using static PawFund.Contract.Services.Event.Respone;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

    public interface IEventRepository : IGenericRepository<Event>
    {
    public Task<IEnumerable<Event>> GetAll();

    Task<PagedResult<Event>> GetAllEventAsync(int pageIndex, int pageSize, EventFilter filterParams, string[] selectedColumns);

    Task<PagedResult<Event>> GetAllEventByStaff(List<Guid> listsBranchId, int pageIndex, int pageSize, EventFilter filterParams, string[] selectedColumns);

}

