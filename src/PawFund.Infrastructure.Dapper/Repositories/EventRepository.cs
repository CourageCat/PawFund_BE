using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;
public class EventRepository : IEventRepository
{
    private readonly IConfiguration _configuration;
    public EventRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(Event entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Event entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Event>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Event>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Event entity)
    {
        throw new NotImplementedException();
    }
}

