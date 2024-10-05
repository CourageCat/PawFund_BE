using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class VolunteerApplication : IVolunteerApplication
{
    private readonly IConfiguration _configuration;
    public VolunteerApplication(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(Domain.Entities.VolunteerApplication entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Domain.Entities.VolunteerApplication entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Domain.Entities.VolunteerApplication>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.VolunteerApplication>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Domain.Entities.VolunteerApplication entity)
    {
        throw new NotImplementedException();
    }
}

