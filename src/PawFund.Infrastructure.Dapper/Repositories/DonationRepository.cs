using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class DonationRepository : IDonationRepository
{

    private readonly IConfiguration _configuration;

    public DonationRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Donation entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Donation entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Donation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Donation> GetByIdAsync(Guid id)
    {

        var sql = "SELECT Id, Amount, Description, OrderId, AccountId, PaymentMethodId, CreatedDate, ModifiedDate, IsDeleted FROM Donations WHERE Id = @id";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Donation>(sql, new { Id = id });
            return result;
        }
    }

    public async Task<Donation> GetLatestDonationAsync()
    {
        var sql = @"
        SELECT Id, Amount, Description, OrderId, AccountId, PaymentMethodId, CreatedDate, ModifiedDate, IsDeleted 
        FROM Donations 
        ORDER BY CreatedDate DESC";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<Donation>(sql);
            return result;
        }
    }

    public Task<int> UpdateAsync(Donation entity)
    {
        throw new NotImplementedException();
    }
}
