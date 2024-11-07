using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Services.EventActivity;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;
using static PawFund.Contract.Services.EventActivity.Filter;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class EventActivityRepository : IEventActivityRepository
{
    private readonly IConfiguration _configuration;
    public EventActivityRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<int> AddAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<EventActivity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<EventActivity>> GetAllByEventId(
        Guid eventId, int pageIndex, int pageSize, EventActivityFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Define valid columns
            var validColumns = new HashSet<string>
        {
            "a.Id", "a.Name", "a.Quantity", "a.StartDate", "a.Description", "a.Status", "a.NumberOfVolunteer",
            "a.IsDeleted AS IsEventActivityDeleted",
            "e.Id", "e.Name", "e.StartDate", "e.EndDate", "e.Description", "e.MaxAttendees",
            "e.ImagesUrl", "e.Status"
        };

            // Select valid columns, or use all if none are specified
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            // Build main query with filtering by event
            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString} 
            FROM EventActivities a
            LEFT JOIN Events e ON a.EventId = e.Id
            WHERE a.EventId = @EventId");

            var parameters = new DynamicParameters();
            parameters.Add("EventId", eventId);

            // Apply filters
            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                queryBuilder.Append(" AND a.Name LIKE @ActivityName");
                parameters.Add("ActivityName", $"%{filterParams.Name}%");
            }

            if (filterParams.Status.HasValue)
            {
                queryBuilder.Append(" AND a.Status = @Status");
                parameters.Add("Status", filterParams.Status.Value);
            }

            // Ensure pageIndex and pageSize are within valid ranges
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            // Count total items after filtering
            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1)
            FROM EventActivities a
            LEFT JOIN Events e ON a.EventId = e.Id
            WHERE a.EventId = @EventId");

            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                totalCountQuery.Append(" AND a.Name LIKE @ActivityName");
            }
            if (filterParams.Status.HasValue)
            {
                totalCountQuery.Append(" AND a.Status = @Status");
            }

            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Calculate total pages and offset
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var offset = (pageIndex - 1) * pageSize;

            // Append pagination and ordering
            queryBuilder.Append($" ORDER BY a.StartDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            // Execute the main query
            var items = (await connection.QueryAsync<EventActivity, Event, EventActivity>(
                queryBuilder.ToString(),
                (activity, eventEntity) =>
                {
                    activity.Event = eventEntity;
                    return activity;
                },
                parameters,
                splitOn: "Id")).ToList();

            // Return paged result
            return new PagedResult<EventActivity>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }


    public async Task<IEnumerable<EventActivity>> GetApprovedEventsActivityId(Guid id)
    {
        var sql = @"
SELECT 
    ea.Id, ea.Name, ea.Quantity, ea.StartDate, ea.Description, ea.Status, ea.NumberOfVolunteer, ea.IsDeleted as IsEvenActivitytDelete,
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description,  e.MaxAttendees, e.IsDeleted as IsEventDeleted
FROM EventActivities ea
JOIN Events e ON e.Id = ea.EventId
WHERE ea.EventId = @Id AND ea.Status = 1";  // Thêm điều kiện Status = false (0)

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<EventActivity, Event, EventActivity>(
                sql,
                (EventActivity, Event) =>
                {
                    EventActivity.Event = Event;
                    return EventActivity;
                },
                new { Id = id },
                splitOn: "IsEvenActivitytDelete"
            );

            return result;
        }
    }

    public async Task<EventActivity>? GetByIdAsync(Guid Id)
    {
        var sql = @"
SELECT 
    ea.Id, ea.Name, ea.Quantity, ea.StartDate, ea.Description, ea.Status, ea.IsDeleted as IsEvenActivitytDelete,
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted as IsEventDeleted
FROM EventActivities ea
JOIN Events e ON e.Id = ea.EventId
WHERE ea.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<EventActivity, Event, EventActivity>(
                sql,
                (EventActivity, Event) =>
                {
                    EventActivity.Event = Event;
                    return EventActivity;
                },
                new { Id = Id },
                splitOn: "IsEvenActivitytDelete"
            );

            return result.FirstOrDefault();
        }
    }



    public Task<PagedResult<EventActivity>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(EventActivity entity)
    {
        throw new NotImplementedException();
    }
}

