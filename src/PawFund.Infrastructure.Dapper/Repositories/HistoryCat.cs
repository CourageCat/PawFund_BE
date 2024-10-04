using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class HistoryCat : IHistoryCat
{
    private readonly IConfiguration _configuration;
    public HistoryCat(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(IHistoryCat entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(IHistoryCat entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IHistoryCat>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IHistoryCat>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(IHistoryCat entity)
    {
        throw new NotImplementedException();
    }
}

