using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Branchs;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Data.Common;
using System.Text;

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

    public async Task<List<Guid>> GetAllBranchByAccountId(Guid id)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            string query = "SELECT Id FROM Branchs WHERE AccountId = @AccountId";

            var branchIds = await connection.QueryAsync<Guid>(query, new { AccountId = id });
            return branchIds.ToList();
        }
    }

    public async Task<Branch>? GetByIdAsync(Guid id)
    {
        var sql = @"
        SELECT b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, b.Province, b.PostalCode, b.AccountId, b.ImageUrl, b.PublicImageId
        FROM Branchs b
        WHERE b.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Branch>(
                sql,
                new { Id = id }
                );

            return result.FirstOrDefault();
        }
    }

    public async Task<PagedResult<Branch>> GetPagedAsync(int pageIndex, int pageSize, Filter.BranchFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Valid columns for selecting
            var validColumns = new HashSet<string> { "Id", "Name", "PhoneNumberOfBranch", "EmailOfBranch", "Description", "NumberHome", "StreetName", "Ward", "District", "Province", "PostalCode", "AccountId", "ImageUrl", "PublicImageId", "IsDeleted" };
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();

            // If no selected columns, select all
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : "*";

            // Start building the query
            var queryBuilder = new StringBuilder($"SELECT {selectedColumnsString} FROM Branchs WHERE 1=1 AND IsDeleted = 0");

            var parameters = new DynamicParameters();

            parameters.Add("BranchId", filterParams.Id);

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
        SELECT COUNT(1) 
        FROM Branchs b
        WHERE 1=1 AND IsDeleted = 0");

            // Filter by Id
            if (filterParams?.Id.HasValue == true)
            {
                queryBuilder.Append(" AND Id = @Id");
                totalCountQuery.Append(" AND Id = @Id");
                parameters.Add("Id", $"{filterParams.Id}");
            }

            // Filter by Id
            if (filterParams?.AccountId.HasValue == true)
            {
                queryBuilder.Append(" AND AccountId = @AccountId");
                totalCountQuery.Append(" AND AccountId = @AccountId");
                parameters.Add("AccountId", $"{filterParams.AccountId}");
            }

            // Filter by Id
            if (filterParams?.Id.HasValue == true)
            {
                queryBuilder.Append(" AND Id = @Id");
                totalCountQuery.Append(" AND Id = @Id");
                parameters.Add("Id", $"{filterParams.Id}");
            }

            // Filter by Name
            if (!string.IsNullOrEmpty(filterParams?.Name))
            {
                queryBuilder.Append(" AND Name LIKE @Name");
                totalCountQuery.Append(" AND Name LIKE @Name");
                parameters.Add("Name", $"%{filterParams.Name}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.PhoneNumberOfBranch))
            {
                queryBuilder.Append(" AND PhoneNumberOfBranch LIKE @PhoneNumberOfBranch");
                totalCountQuery.Append(" AND PhoneNumberOfBranch LIKE @PhoneNumberOfBranch");
                parameters.Add("PhoneNumberOfBranch", $"%{filterParams.PhoneNumberOfBranch}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.EmailOfBranch))
            {
                queryBuilder.Append(" AND EmailOfBranch LIKE @EmailOfBranch");
                totalCountQuery.Append(" AND EmailOfBranch LIKE @EmailOfBranch");
                parameters.Add("EmailOfBranch", $"%{filterParams.EmailOfBranch}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.Description))
            {
                queryBuilder.Append(" AND Description LIKE @Description");
                totalCountQuery.Append(" AND Description LIKE @Description");
                parameters.Add("Description", $"%{filterParams.Description}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.NumberHome))
            {
                queryBuilder.Append(" AND NumberHome LIKE @NumberHome");
                totalCountQuery.Append(" AND NumberHome LIKE @NumberHome");
                parameters.Add("NumberHome", $"%{filterParams.NumberHome}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.StreetName))
            {
                queryBuilder.Append(" AND StreetName LIKE @StreetName");
                totalCountQuery.Append(" AND StreetName LIKE @StreetName");
                parameters.Add("StreetName", $"%{filterParams.StreetName}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.Ward))
            {
                queryBuilder.Append(" AND Ward LIKE @Ward");
                totalCountQuery.Append(" AND Ward LIKE @Ward");
                parameters.Add("Ward", $"%{filterParams.Ward}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.District))
            {
                queryBuilder.Append(" AND District LIKE @District");
                totalCountQuery.Append(" AND District LIKE @District");
                parameters.Add("District", $"%{filterParams.District}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.Province))
            {
                queryBuilder.Append(" AND Province LIKE @Province");
                totalCountQuery.Append(" AND Province LIKE @Province");
                parameters.Add("Province", $"%{filterParams.Province}%");
            }

            if (!string.IsNullOrEmpty(filterParams?.PostalCode))
            {
                queryBuilder.Append(" AND PostalCode LIKE @PostalCode");
                totalCountQuery.Append(" AND PostalCode LIKE @PostalCode");
                parameters.Add("PostalCode", $"%{filterParams.PostalCode}%");
            }

            //Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            //Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<Branch>(paginatedQuery, parameters)).ToList();

            return new PagedResult<Branch>(items, pageIndex, pageSize, totalCount, totalPages);

        }
    }

    public Task<int> UpdateAsync(Branch entity)
    {
        throw new NotImplementedException();
    }
}

