using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.VolunteerApplicationDetail.Filter;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class VolunteerApplicationDetailRepository : IVolunteerApplicationDetail
{
    private readonly IConfiguration _configuration;
    public VolunteerApplicationDetailRepository(IConfiguration configuration)
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

    public async Task<VolunteerApplicationDetail?> GetByIdAsync(Guid id)
    {
        var sql = @"
        SELECT 
            vad.Id, vad.Status, vad.Description, vad.ReasonReject, vad.EventId, 
            vad.EventActivityId, vad.AccountId, vad.CreatedDate, vad.ModifiedDate, vad.IsDeleted,
            acc.Id AS AccountId, acc.FirstName, acc.LastName, acc.Email, acc.PhoneNumber, acc.Status
        FROM VolunteerApplicationDetails vad
        JOIN Accounts acc ON vad.AccountId = acc.Id
        WHERE vad.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<VolunteerApplicationDetail, Account, VolunteerApplicationDetail>(
                sql,
                (volunteerApplicationDetail, account) =>
                {
                    volunteerApplicationDetail.Account = account;
                    return volunteerApplicationDetail;
                },
                new { Id = id },
                splitOn: "FirstName" // Thay đổi splitOn để tránh trùng lặp với Id
            );

            return result.FirstOrDefault();
        }
    }



    public Task<PagedResult<Domain.Entities.VolunteerApplicationDetail>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Domain.Entities.VolunteerApplicationDetail entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckVolunteerApplicationExists(Guid eventId, Guid accountId)
    {
        var sql = @"
SELECT COUNT(1)
FROM VolunteerApplicationDetails
WHERE EventId = @EventId AND AccountId = @AccountId";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var exists = await connection.ExecuteScalarAsync<int>(sql, new { EventId = eventId, AccountId = accountId });
            return exists > 0;
        }
    }

    public async Task<PagedResult<VolunteerApplicationDetail>> GetAllVolunteerAppicationByActivityIdAsync(Guid id, int pageIndex, int pageSize, VolunteerApplicationFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Define the valid columns for selection
            var validColumns = new HashSet<string>
        {
            "vad.Id", "vad.Status", "vad.Description", "vad.ReasonReject", "vad.EventId",
            "vad.EventActivityId", "vad.AccountId", "vad.CreatedDate", "vad.ModifiedDate", "vad.IsDeleted"
        };

            // Select columns based on user input or use all valid columns by default
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            // Build the main query with filtering by EventActivityId
            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString}
            FROM VolunteerApplicationDetails vad
            WHERE vad.EventActivityId = @EventActivityId");

            var parameters = new DynamicParameters();
            parameters.Add("EventActivityId", id);

            // Ensure pageIndex and pageSize are within expected bounds
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            // Build the query to count total records matching the filter
            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1)
            FROM VolunteerApplicationDetails vad
            WHERE vad.EventActivityId = @EventActivityId");

            // Optional filter for Status if provided
            if (filterParams.Status.HasValue)
            {
                queryBuilder.Append(" AND vad.Status = @Status");
                totalCountQuery.Append(" AND vad.Status = @Status");
                parameters.Add("Status", filterParams.Status.Value);
            }

            // Get total count for pagination
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Calculate total pages
            var totalPages = Math.Ceiling(totalCount / (double)pageSize);

            // Apply pagination
            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY vad.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            // Fetch paginated data
            var items = (await connection.QueryAsync<VolunteerApplicationDetail>(
                paginatedQuery,
                parameters
            )).ToList();

            // Return the paginated result
            return new PagedResult<VolunteerApplicationDetail>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }
}

