using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.AdoptApplications.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

    public interface IEventActivityRepository : IGenericRepository<EventActivity>
    {
    public Task<IEnumerable<EventActivity>> GetAllByEventId(Guid id);

    public Task<IEnumerable<EventActivity>> GetApprovedEventsActivityId(Guid id);

    
}

