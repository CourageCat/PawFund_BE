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
}

