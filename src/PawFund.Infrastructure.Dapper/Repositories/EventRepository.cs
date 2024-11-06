using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.Event;
using PawFund.Contract.Services.Event;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.DTOs.Event.GetEventByIdDTO;
using static PawFund.Contract.Services.Event.Filter;
using static PawFund.Contract.Services.Event.Respone;

namespace PawFund.Infrastructure.Dapper.Repositories;
public class EventRepository : IEventRepository
{
    private readonly IConfiguration _configuration;
    public EventRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Event entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Event entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        var sql = @"
SELECT 
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted, e.Status as IsEventDelete,
    b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, b.Province, b.PostalCode, b.IsDeleted as IsBranchDeleted
FROM Events e
JOIN Branchs b ON b.Id = e.BranchId";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Event, Branch, Event>(
                sql,
                (Event, Branch) =>
                {
                    Event.Branch = Branch;
                    return Event;
                },
                splitOn: "IsEventDelete"
            );

            return result;
        }
    }

    public async Task<PagedResult<Event>> GetAllEventAsync(
      int pageIndex, int pageSize, EventFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "e.Id", "e.Name", "e.StartDate", "e.EndDate", "e.Description", "e.MaxAttendees",
            "e.ImagesUrl", "e.Status", // Thêm Status từ bảng Events
            "b.Id", "b.Name", "b.PhoneNumberOfBranch", "b.EmailOfBranch",
            "b.Description", "b.NumberHome", "b.StreetName", "b.Ward", "b.District", "b.Province"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
        SELECT {selectedColumnsString} 
        FROM Events e
        LEFT JOIN Branchs b ON e.BranchId = b.Id
        WHERE 1=1");

            var parameters = new DynamicParameters();

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
        SELECT COUNT(1) 
        FROM Events e
        LEFT JOIN Branchs b ON e.BranchId = b.Id
        WHERE 1=1");

            // Filter by Event Name
            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                queryBuilder.Append(" AND e.Name LIKE @EventName");
                totalCountQuery.Append(" AND e.Name LIKE @EventName");
                parameters.Add("EventName", $"%{filterParams.Name}%");
            }

            // Filter by Event Status
            if (filterParams.Status.HasValue)
            {
                queryBuilder.Append(" AND e.Status = @Status");
                totalCountQuery.Append(" AND e.Status = @Status");
                parameters.Add("Status", filterParams.Status.Value);
            }

            // Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY e.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<Event, Branch, Event>(
                paginatedQuery,
                (eventEntity, branch) =>
                {
                    eventEntity.Branch = branch;

                    // Chuyển đổi Status từ số nguyên sang Enum EventStatus
                    if (Enum.IsDefined(typeof(EventStatus), eventEntity.Status))
                    {
                        eventEntity.Status = (EventStatus)eventEntity.Status;
                    }
                    else
                    {
                        eventEntity.Status = EventStatus.NotStarted; // or a default/fallback status
                    }

                    return eventEntity;
                },
                parameters,
                splitOn: "Id"
            )).ToList();

            return new PagedResult<Event>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }

    public async Task<PagedResult<Event>> GetAllEventByStaff(List<Guid> listsBranchId, int pageIndex, int pageSize, EventFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "e.Id", "e.Name", "e.StartDate", "e.EndDate", "e.Description", "e.MaxAttendees",
            "e.ImagesUrl", "e.Status",
            "b.Id", "b.Name", "b.PhoneNumberOfBranch", "b.EmailOfBranch",
            "b.Description", "b.NumberHome", "b.StreetName", "b.Ward", "b.District", "b.Province"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString} 
            FROM Events e
            LEFT JOIN Branchs b ON e.BranchId = b.Id
            WHERE 1=1");

            var parameters = new DynamicParameters();

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1) 
            FROM Events e
            LEFT JOIN Branchs b ON e.BranchId = b.Id
            WHERE 1=1");

            // Filter by branch IDs
            if (listsBranchId != null && listsBranchId.Any())
            {
                queryBuilder.Append(" AND e.BranchId IN @BranchIds");
                totalCountQuery.Append(" AND e.BranchId IN @BranchIds");
                parameters.Add("BranchIds", listsBranchId);
            }

            // Filter by Event Name
            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                queryBuilder.Append(" AND e.Name LIKE @EventName");
                totalCountQuery.Append(" AND e.Name LIKE @EventName");
                parameters.Add("EventName", $"%{filterParams.Name}%");
            }

            // Filter by Event Status
            if (filterParams.Status.HasValue)
            {
                queryBuilder.Append(" AND e.Status = @Status");
                totalCountQuery.Append(" AND e.Status = @Status");
                parameters.Add("Status", filterParams.Status.Value);
            }

            // Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY e.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<Event, Branch, Event>(
                paginatedQuery,
                (eventEntity, branch) =>
                {
                    eventEntity.Branch = branch;

                    // Convert Status from integer to Enum EventStatus
                    if (Enum.IsDefined(typeof(EventStatus), eventEntity.Status))
                    {
                        eventEntity.Status = (EventStatus)eventEntity.Status;
                    }
                    else
                    {
                        eventEntity.Status = EventStatus.NotStarted; // or a default/fallback status
                    }

                    return eventEntity;
                },
                parameters,
                splitOn: "Id"
            )).ToList();

            return new PagedResult<Event>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }

    public async Task<IEnumerable<Event>> GetAllNotApproved()
    {
        var sql = @"
SELECT 
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted as IsEventDelete,
    b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, b.Province, b.PostalCode, b.IsDeleted as IsBranchDeleted
FROM Events e
JOIN Branchs b ON b.Id = e.BranchId
WHERE e.Status = @NotApprovedStatus";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Event, Branch, Event>(
                sql,
                (Event, Branch) =>
                {
                    Event.Branch = Branch;
                    return Event;
                },
                new { NotApprovedStatus = (int)EventStatus.NotApproved },  // Truyền giá trị enum vào tham số
                splitOn: "IsEventDelete"
            );

            return result;
        }
    }

    public async Task<Event>? GetByIdAsync(Guid Id)
    {
        var sql = @"
SELECT 
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, 
    e.IsDeleted, e.Status as IsEventDelete, 
    e.ThumbHeroUrl, e.ImagesUrl,
    b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, 
    b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, 
    b.Province, b.PostalCode, b.IsDeleted as IsBranchDeleted
FROM Events e
JOIN Branchs b ON b.Id = e.BranchId
WHERE e.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Event, Branch, Event>(
                sql,
                (eventObj, branch) =>
                {
                    Console.WriteLine($"ThumbHeroUrl: {eventObj.ThumbHeroUrl}, ImagesUrl: {eventObj.ImagesUrl}");
                    eventObj.Branch = branch;
                    return eventObj;
                },
                new { Id = Id },
                splitOn: "Id"  // Sửa thành "Id" của Branch
            );

            return result.FirstOrDefault();
        }
    }



    public Task<int> UpdateAsync(Event entity)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyCollection<Event>> IGenericRepository<Event>.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}

