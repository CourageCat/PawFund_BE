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

public class HistoryCatRepository : IHistoryCatRepository
{
    private readonly IConfiguration _configuration;
    public HistoryCatRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(HistoryCat entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(HistoryCat entity)
    {
        throw new NotImplementedException();
    }

    public async Task<HistoryCat>? GetByIdAsync(Guid Id)
    {

        var sql = @"
        SELECT c.Id, c.DateAdopt, c.CatId, c.AccountId
        FROM HistoryCats c
        WHERE c.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<HistoryCat>(
                sql,
                new { Id = Id }
                );

            return result.FirstOrDefault();
        }
    }

    public Task<PagedResult<IHistoryCatRepository>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(HistoryCat entity)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyCollection<HistoryCat>> IGenericRepository<HistoryCat>.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}

