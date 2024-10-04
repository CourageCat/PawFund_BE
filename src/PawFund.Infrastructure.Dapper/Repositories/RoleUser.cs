using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class RoleUser : IRoleUser
{
    private readonly IConfiguration _configuration;
    public RoleUser(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(IRoleUser entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(IRoleUser entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IRoleUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IRoleUser>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(IRoleUser entity)
    {
        throw new NotImplementedException();
    }
}

