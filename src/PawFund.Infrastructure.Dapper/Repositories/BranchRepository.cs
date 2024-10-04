using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly IConfiguration _configuration;
    public BranchRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(Branch entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Branch entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Branch>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Branch>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Branch entity)
    {
        throw new NotImplementedException();
    }
}

