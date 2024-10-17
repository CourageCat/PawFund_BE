using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
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

    public async Task<Branch>? GetByIdAsync(Guid Id)
    {
        var sql = @"
        SELECT b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, b.Province, b.PostalCode, b.AccountId
        FROM Branchs b
        WHERE b.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Branch>(
                sql,
                new { Id = Id }
                );

            return result.FirstOrDefault();
        }
    }

    public Task<PagedResult<Branch>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Branch entity)
    {
        throw new NotImplementedException();
    }
}

