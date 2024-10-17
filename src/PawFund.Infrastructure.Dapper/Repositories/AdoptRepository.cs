using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static PawFund.Contract.Services.AdoptApplications.Filter;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class AdoptRepository : IAdoptRepository
{
    private readonly IConfiguration _configuration;

    public AdoptRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(AdoptPetApplication entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(AdoptPetApplication entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasAccountRegisterdWithPetAsync(Guid accountId, Guid catId)
    {
        var sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM AdoptPetApplications WHERE AccountId = @AccountId AND CatId = @CatId AND IsDeleted = 0 AND Status != -1) THEN 1 ELSE 0 END";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<bool>(sql, new { AccountId = accountId, CatId = catId });
            return result;
        }
    }

    public Task<IReadOnlyCollection<AdoptPetApplication>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AdoptPetApplication?> GetByIdAsync(Guid Id)
    {
        var sql = @"
        SELECT
            a.Id, a.MeetingDate, a.ReasonReject, a.Status, a.IsFinalized, a.Description, a.CreatedDate, a.IsDeleted as IsAdoptDeleted,
            acc.Id, acc.FirstName, acc.LastName, acc.Email, acc.PhoneNumber, acc.IsDeleted as IsAccountDeleted,
            c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Size, c.Color, c.Description
        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        WHERE a.Id = @Id AND a.IsDeleted = 0";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<AdoptPetApplication, Account, Cat, AdoptPetApplication>(
                sql,
                (adoptPetApplication, account, cat) =>
                {
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;
                    return adoptPetApplication;
                },
                new { Id = Id },
                splitOn: "IsAdoptDeleted,IsAccountDeleted"
            );

            return result.FirstOrDefault();
        }
    }


    public Task<int> UpdateAsync(AdoptPetApplication entity)
    {
        throw new NotImplementedException();
    }

    //public async Task<PagedResult<AdoptPetApplication>> GetAllApplicationsAsync(int pageIndex, int pageSize, bool isAscCreatedDate, string[] selectedColumns)
    //{
    //    using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
    //    {
    //        // Define valid columns to prevent SQL injection
    //        var validColumns = new HashSet<string>
    //    {
    //        "a.Id", "a.MeetingDate", "a.ReasonReject", "a.Status", "a.IsFinalized", "a.Description", "a.CreatedDate", "a.IsDeleted as IsAdoptDeleted",
    //        "acc.Id", "acc.FirstName", "acc.LastName", "acc.Email", "acc.PhoneNumber", "acc.IsDeleted as IsAccountDeleted",
    //        "c.Id", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Size", "c.Color", "c.Description as CatDescription"
    //    };

    //        // Filter selected columns based on valid columns
    //        var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();

    //        // If no columns are selected, use all columns
    //        var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

    //        // Build the query with joins and selected columns
    //        var queryBuilder = new StringBuilder($@"
    //    SELECT {selectedColumnsString} 
    //    FROM AdoptPetApplications a
    //    JOIN Accounts acc ON acc.Id = a.AccountId
    //    JOIN Cats c ON c.Id = a.CatId
    //    WHERE 1=1");

    //        //// Add sorting by CreatedDate
    //        //queryBuilder.Append($" ORDER BY a.CreatedDate {(isAscCreatedDate ? "ASC" : "DESC")} ");



    //        var parameters = new DynamicParameters();

    //        pageIndex = pageIndex <= 0 ? 1 : pageIndex;
    //        pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

    //        // Total record count
    //        var totalCountQuery = $@"
    //SELECT COUNT(1) 
    //FROM AdoptPetApplications a
    //JOIN Accounts acc ON acc.Id = a.AccountId
    //JOIN Cats c ON c.Id = a.CatId
    //WHERE a.IsDeleted = 0";
    //        var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery, parameters);

    //        // Pagination with parameterized query
    //        var offset = (pageIndex - 1) * pageSize;
    //        var paginatedQuery = $"{queryBuilder} ORDER BY a.CreatedDate {(isAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

    //        var items = (await connection.QueryAsync<AdoptPetApplication, Account, Cat, AdoptPetApplication>(paginatedQuery, (adoptPetApplication, account, cat) =>
    //        {
    //            adoptPetApplication.Account = account;
    //            adoptPetApplication.Cat = cat;
    //            return adoptPetApplication;
    //        }, splitOn: "IsAdoptDeleted,IsAccountDeleted"
    //        )).ToList();
    //        //// Use PagedResult.CreateAsync to execute the query and handle paging
    //        //return await PagedResult<AdoptPetApplication>.CreateAsync(
    //        //    connection,
    //        //    queryBuilder.ToString(),
    //        //    parameters,
    //        //    pageIndex,
    //        //    pageSize
    //        //);
    //        return new PagedResult<AdoptPetApplication>(items, pageIndex, pageSize, totalCount);

    //    }
    //}

    public async Task<PagedResult<AdoptPetApplication>> GetAllApplicationsByAdopterAsync(
    Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "a.Id", "a.MeetingDate", "a.ReasonReject", "a.Status", "a.IsFinalized", "a.Description", "a.CreatedDate", "a.IsDeleted as IsAdoptDeleted",
            "acc.Id", "acc.FirstName", "acc.LastName", "acc.Email", "acc.PhoneNumber", "acc.IsDeleted as IsAccountDeleted",
            "c.Id", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Size", "c.Color", "c.Description as CatDescription"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
        SELECT {selectedColumnsString} 
        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        WHERE 1=1 AND a.AccountId = @AccountId");

            var parameters = new DynamicParameters();
            parameters.Add("AccountId", accountId);

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
        SELECT COUNT(1) 
        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        WHERE a.IsDeleted = 0 AND a.AccountId = @AccountId");

            // Use the table alias 'a' for Status
            if (filterParams?.Status.HasValue == true)
            {
                queryBuilder.Append(" AND a.Status = @Status");
                totalCountQuery.Append(" AND a.Status = @Status");
                parameters.Add("Status", filterParams.Status);
            }

            //Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            //Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY a.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<AdoptPetApplication, Account, Cat, AdoptPetApplication>(
                paginatedQuery,
                (adoptPetApplication, account, cat) =>
                {
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;
                    return adoptPetApplication;
                },
                parameters,
                splitOn: "IsAdoptDeleted,IsAccountDeleted"
            )).ToList();

            return new PagedResult<AdoptPetApplication>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }


    public async Task<PagedResult<AdoptPetApplication>> GetAllApplicationByStaffAsync(Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Define valid columns to prevent SQL injection
            var validColumns = new HashSet<string>
        {
            "a.Id", "a.MeetingDate", "a.ReasonReject", "a.Status", "a.IsFinalized", "a.Description", "a.CreatedDate", "a.IsDeleted as IsAdoptDeleted",
            "acc.Id", "acc.FirstName", "acc.LastName", "acc.Email", "acc.PhoneNumber", "acc.IsDeleted as IsAccountDeleted",
            "c.Id", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Size", "c.Color", "c.Description as CatDescription"
        };

            // Filter selected columns based on valid columns
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            // Build the query with parameterized AccountId
            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString} 
            FROM AdoptPetApplications a
            JOIN Accounts acc ON acc.Id = a.AccountId
            JOIN Cats c ON c.Id = a.CatId
            JOIN Branchs b ON b.Id = c.BranchId
            WHERE 1=1 AND b.AccountId = @AccountId");

            var parameters = new DynamicParameters();
            parameters.Add("AccountId", accountId);

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            // Total record count
            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1) 
            FROM AdoptPetApplications a
            JOIN Accounts acc ON acc.Id = a.AccountId
            JOIN Cats c ON c.Id = a.CatId
            JOIN Branchs b ON b.Id = c.BranchId
            WHERE a.IsDeleted = 0 AND b.AccountId = @AccountId");
            // Use the table alias 'a' for Status
            if (filterParams?.Status.HasValue == true)
            {
                queryBuilder.Append(" AND a.Status = @Status");
                totalCountQuery.Append(" AND a.Status = @Status");
                parameters.Add("Status", filterParams.Status);
            }
            //Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            //Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            // Pagination with parameterized query
            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY a.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<AdoptPetApplication, Account, Cat, AdoptPetApplication>(
                paginatedQuery,
                (adoptPetApplication, account, cat) =>
                {
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;
                    return adoptPetApplication;
                },
                parameters,
                splitOn: "IsAdoptDeleted,IsAccountDeleted"
            )).ToList();

            return new PagedResult<AdoptPetApplication>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }
}

