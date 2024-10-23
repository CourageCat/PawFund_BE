using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<PagedResult<EventActivity>> GetAllByEventId(Guid eventId, int pageIndex, int pageSize, bool filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "ea.Id", "ea.Name", "ea.Quantity", "ea.StartDate", "ea.Description", "ea.Status", "ea.IsDeleted as IsEventActivityDeleted",
            "e.Id", "e.Name", "e.StartDate", "e.EndDate", "e.Description", "e.MaxAttendees", "e.IsDeleted as IsEventDeleted"
        };

            // Lọc các cột được chọn nếu có, hoặc sử dụng tất cả các cột hợp lệ
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
SELECT {selectedColumnsString} 
FROM EventActivities ea
JOIN Events e ON e.Id = ea.EventId
WHERE ea.EventId = @EventId");

            var parameters = new DynamicParameters();
            parameters.Add("EventId", eventId);

            // Thiết lập phân trang
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            // Truy vấn đếm tổng số bản ghi để hỗ trợ phân trang
            var totalCountQuery = new StringBuilder($@"
SELECT COUNT(1) 
FROM EventActivities ea
JOIN Events e ON e.Id = ea.EventId
WHERE ea.IsDeleted = 0 AND ea.EventId = @EventId");

            // Áp dụng bộ lọc Status theo giá trị bool của filterParams
            if (filterParams)
            {
                queryBuilder.Append(" AND ea.Status = 1");
                totalCountQuery.Append(" AND ea.Status = 1");
            }
            else
            {
                queryBuilder.Append(" AND ea.Status = 0");
                totalCountQuery.Append(" AND ea.Status = 0");
            }

            // Lấy tổng số bản ghi
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Tính tổng số trang
            var totalPages = totalCount == 0 ? 1 : Math.Ceiling(totalCount / (double)pageSize);

            // Logic phân trang: Tính offset
            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY ea.StartDate {(filterParams ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            // Thực thi truy vấn và ánh xạ kết quả về các thực thể
            var items = (await connection.QueryAsync<EventActivity, Event, EventActivity>(
                paginatedQuery,
                (eventActivity, eventEntity) =>
                {
                    eventActivity.Event = eventEntity; // Ánh xạ event vào đối tượng eventActivity
                    return eventActivity;
                },
                parameters,
                splitOn: "IsEventActivityDeleted,IsEventDeleted" // Đảm bảo các cột split đúng với alias trong truy vấn
            )).ToList();

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

