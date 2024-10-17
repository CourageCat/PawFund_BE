using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Accounts;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Text;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IConfiguration _configuration;

    public AccountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool>? EmailExistSystem(string email)
    {
        var sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM Accounts WHERE Email = @Email) THEN 1 ELSE 0 END";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
            return result;
        }
    }

    public async Task<List<Account>> GetListUser()
    {
        var sql = "SELECT * FROM Accounts";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<Account>(
                    sql);
            return (List<Account>)result;
        }
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Password, RoleId, LoginType, AvatarUrl, IsDeleted FROM Accounts WHERE Email = @Email";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Account>(sql, new { Email = email });
            return result;
        }
    }

    public async Task<Account> GetByIdAsync(Guid id)
    {
        var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Password, RoleId, LoginType, AvatarUrl, IsDeleted FROM Accounts WHERE Id = @id";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Account>(sql, new { Id = id });
            return result;
        }
    }

    public Task<PagedResult<Account>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Account>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<Account>> GetPagedAsync(int pageIndex, int pageSize, Filter.AccountFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Valid columns for selecting
            var validColumns = new HashSet<string> { "Id", "FirstName", "LastName", "Email", "PhoneNumber", "Status", "RoleId" };
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();

            // If no selected columns, select all
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : "*";

            // Start building the query
            var queryBuilder = new StringBuilder($"SELECT {selectedColumnsString} FROM Accounts WHERE 1=1");

            var parameters = new DynamicParameters();

            // Filter by FirstName
            if (!string.IsNullOrEmpty(filterParams?.FirstName))
            {
                queryBuilder.Append(" AND FirstName LIKE @FirstName");
                parameters.Add("FirstName", $"%{filterParams.FirstName}%");
            }

            // Filter by Status (e.g., true/false)
            if (filterParams?.Status.HasValue == true)
            {
                queryBuilder.Append(" AND Status = @Status");
                parameters.Add("Status", filterParams.Status.Value);
            }

            if (filterParams?.RoleType.HasValue == true)
            {
                queryBuilder.Append(" AND RoleId = @RoleId");
                parameters.Add("RoleId", (int)filterParams.RoleType.Value);  // Cast RoleType enum to its integer value
            }

            return await PagedResult<Account>.CreateAsync(connection, queryBuilder.ToString(), parameters, pageIndex, pageSize);
        }
    }
}
