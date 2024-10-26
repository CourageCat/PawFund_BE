using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Donates;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Text;

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

    public async Task<PagedResult<Donation>> GetPagedDonatesAsync(int pageIndex, int pageSize, Filter.DonateFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "d.Id", "d.Amount", "d.Description", "d.OrderId", "d.PaymentMethodId", "d.CreatedDate", "d.ModifiedDate", "d.IsDeleted",
            "ac.FirstName", "ac.LastName"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString}
            FROM Donations d
            JOIN Accounts ac ON d.AccountId = ac.Id
            WHERE 1=1");

            var parameters = new DynamicParameters();

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1) 
            FROM Donations d
            JOIN Accounts ac ON d.AccountId = ac.Id
            WHERE 1=1");

            // Filter by AccountId
            if (filterParams?.UserId.HasValue == true)
            {
                queryBuilder.Append(" AND d.AccountId = @UserId");
                totalCountQuery.Append(" AND d.AccountId = @UserId");
                parameters.Add("UserId", filterParams.UserId.Value);
            }

            // Filter by PaymentMethod
            if (filterParams?.PaymentMethodType.HasValue == true)
            {
                queryBuilder.Append(" AND d.PaymentMethodId = @PaymentMethodId");
                totalCountQuery.Append(" AND d.PaymentMethodId = @PaymentMethodId");
                parameters.Add("PaymentMethodId", (int)filterParams.PaymentMethodType.Value);
            }

            // Filter by MinAmount
            if (filterParams?.MinAmount.HasValue == true)
            {
                queryBuilder.Append(" AND d.Amount >= @MinAmount");
                totalCountQuery.Append(" AND d.Amount >= @MinAmount");
                parameters.Add("MinAmount", filterParams.MinAmount.Value);
            }

            // Filter by MaxAmount
            if (filterParams?.MaxAmount.HasValue == true)
            {
                queryBuilder.Append(" AND d.Amount <= @MaxAmount");
                totalCountQuery.Append(" AND d.Amount <= @MaxAmount");
                parameters.Add("MaxAmount", filterParams.MaxAmount.Value);
            }

            // Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Calculate TotalPages
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var offset = (pageIndex - 1) * pageSize;
            var orderDirection = filterParams?.IsDateDesc == true ? "DESC" : "ASC"; 

            var paginatedQuery = $"{queryBuilder} ORDER BY d.CreatedDate {orderDirection} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<Donation, Account, Donation>(
                paginatedQuery,
                (donate, account) =>
                {
                    donate.Account = account; // Assuming Donation has a property called Account
                    return donate;
                },
                parameters,
                splitOn: "FirstName" // Ensure this matches the first column in the selectedColumns
            )).ToList();

            return new PagedResult<Donation>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }


    public Task<int> UpdateAsync(Donation entity)
    {
        throw new NotImplementedException();
    }
}
