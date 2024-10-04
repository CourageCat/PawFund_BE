using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class EventActivityRepository : IEventActivityRepository
{
    private readonly IConfiguration _configuration;
    public EventActivityRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<EventActivity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<EventActivity>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }
}

