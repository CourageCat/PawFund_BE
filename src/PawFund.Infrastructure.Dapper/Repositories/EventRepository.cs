using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted as IsEventDelete,
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

    public async Task<Event>? GetByIdAsync(Guid Id)
    {
        var sql = @"
SELECT 
    e.Id, e.Name, e.StartDate, e.EndDate, e.Description, e.MaxAttendees, e.IsDeleted as IsEventDelete,
    b.Id, b.Name, b.PhoneNumberOfBranch, b.EmailOfBranch, b.Description, b.NumberHome, b.StreetName, b.Ward, b.District, b.Province, b.PostalCode, b.IsDeleted as IsBranchDeleted
FROM Events e
JOIN Branchs b ON b.Id = e.BranchId
WHERE e.Id = @Id";

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
                new { Id = Id },
                splitOn: "IsEventDelete"
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

