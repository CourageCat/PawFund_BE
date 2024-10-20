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

    public async Task<IEnumerable<EventActivity>> GetAllByEventId(Guid id)
    {
        var sql = @"
SELECT 
    ea.Id, ea.Name, ea.Quantity, ea.StartDate, ea.Description, ea.Status, ea.IsDeleted as IsEvenActivitytDelete,
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted as IsEventDeleted
FROM EventActivities ea
JOIN Events e ON e.Id = ea.EventId
WHERE ea.EventId = @Id";

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

