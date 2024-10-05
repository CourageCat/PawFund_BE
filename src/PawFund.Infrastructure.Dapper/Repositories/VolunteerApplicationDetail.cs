using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class VolunteerApplicationDetail : IVolunteerApplicationDetail
{
    private readonly IConfiguration _configuration;
    public VolunteerApplicationDetail(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(Domain.Entities.VolunteerApplicationDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Domain.Entities.VolunteerApplicationDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Domain.Entities.VolunteerApplicationDetail>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.VolunteerApplicationDetail>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Domain.Entities.VolunteerApplicationDetail entity)
    {
        throw new NotImplementedException();
    }
}

