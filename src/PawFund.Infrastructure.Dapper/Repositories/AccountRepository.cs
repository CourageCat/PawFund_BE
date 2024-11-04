using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.Authentication;
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

    public async Task<bool>? EmailExistSystemAsync(string email)
    {
        var sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM Accounts WHERE Email = @Email) THEN 1 ELSE 0 END";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
            return result;
        }
    }

    public async Task<bool>? AccountExistSystemAsync(Guid userId)
    {
        var sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM Accounts WHERE Id = @Id) THEN 1 ELSE 0 END";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<bool>(sql, new { Id = userId });
            return result;
        }
    }

    public async Task<List<Account>> GetListUserAsync()
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
        var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Password, RoleId, LoginType, CropAvatarUrl, FullAvatarUrl, IsDeleted FROM Accounts WHERE Email = @Email";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Account>(sql, new { Email = email });
            return result;
        }
    }

    public async Task<Account> GetByIdAsync(Guid id)
    {
        var sql = @"
        SELECT 
            [Id],
            [FirstName],
            [LastName],
            [Email],
            [PhoneNumber],
            [Status],
            [LoginType],
            [Password],
            [CropAvatarUrl],
            [CropAvatarId],
            [FullAvatarUrl],
            [FullAvatarId],
            [RoleId],
            [CreatedDate],
            [ModifiedDate],
            [IsDeleted]
        FROM [Accounts]
        WHERE [Id] = @id"; 
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Account>(sql, new { Id = id });
            return result;
        }
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
            var validColumns = new HashSet<string> { "Id", "FirstName", "LastName", "Email", "PhoneNumber", "Password", "Status", "RoleId", "Gender", "LoginType", "IsDeleted" };
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();

            // If no selected columns, select all
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : "*";

            // Start building the query
            var queryBuilder = new StringBuilder($"SELECT {selectedColumnsString} FROM Accounts WHERE 1=1");

            var parameters = new DynamicParameters();

            // Filter by Id
            if (filterParams?.Id.HasValue == true)
            {
                queryBuilder.Append(" AND Id LIKE @Id");
                parameters.Add("Id", $"%{filterParams.Id}%");
            }

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

    public async Task<PagedResult<Account>> GetUsersPagedAsync(int pageIndex, int pageSize, Filter.AccountsFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Valid columns for selecting
            var validColumns = new HashSet<string>
        {
            "Id", "FirstName", "LastName", "Email", "PhoneNumber", "Password", "Status",
            "Gender", "CropAvatarUrl", "CropAvatarId", "FullAvatarUrl", "FullAvatarId",
            "LoginType", "RoleId", "IsDeleted"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : "*";

            // Start building the query
            var queryBuilder = new StringBuilder($"SELECT {selectedColumnsString} FROM Accounts WHERE IsDeleted IS NULL AND RoleId = @MemberRoleId");
            var totalCountQuery = new StringBuilder("SELECT COUNT(1) FROM Accounts WHERE IsDeleted IS NULL AND RoleId = @MemberRoleId");

            var parameters = new DynamicParameters();
            parameters.Add("MemberRoleId", (int)RoleType.Member);  // Set RoleType.Member (value = 3)

            // Pagination logic
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            // Filter by Id
            if (filterParams?.Id.HasValue == true)
            {
                queryBuilder.Append(" AND Id = @Id");
                totalCountQuery.Append(" AND Id = @Id");
                parameters.Add("Id", filterParams.Id);
            }

            // Filter by FirstName
            if (!string.IsNullOrEmpty(filterParams?.FirstName))
            {
                queryBuilder.Append(" AND FirstName LIKE @FirstName");
                totalCountQuery.Append(" AND FirstName LIKE @FirstName");
                parameters.Add("FirstName", $"%{filterParams.FirstName}%");
            }

            // Filter by LastName
            if (!string.IsNullOrEmpty(filterParams?.LastName))
            {
                queryBuilder.Append(" AND LastName LIKE @LastName");
                totalCountQuery.Append(" AND LastName LIKE @LastName");
                parameters.Add("LastName", $"%{filterParams.LastName}%");
            }

            // Filter by Status
            if (filterParams?.Status.HasValue == true)
            {
                queryBuilder.Append(" AND Status = @Status");
                totalCountQuery.Append(" AND Status = @Status");
                parameters.Add("Status", filterParams.Status);
            }

            // Count Total Records
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);
            var totalPages = Math.Ceiling(totalCount / (double)pageSize);

            // Apply paging
            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            // Execute query and return results
            var items = (await connection.QueryAsync<Account>(paginatedQuery, parameters)).ToList();

            return new PagedResult<Account>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }



}
