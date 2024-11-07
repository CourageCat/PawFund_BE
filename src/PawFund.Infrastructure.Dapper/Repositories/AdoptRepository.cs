using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Text;
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
            c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Weight, c.Color, c.Description, c.Sterilization, c.IsDeleted as IsCatDeleted,
            ic.ImageUrl, ic.PublicImageId

        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        LEFT JOIN ImageCats ic ON c.Id = ic.CatId
        WHERE a.Id = @Id AND a.IsDeleted = 0";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<AdoptPetApplication, Account, Cat, ImageCat, AdoptPetApplication>(
                sql,
                (adoptPetApplication, account, cat, imageCat) =>
                {
                    cat.ImageCats = cat.ImageCats ?? new List<ImageCat>();
                    if (imageCat != null)
                    {
                        cat.ImageCats.Add(imageCat);
                    }
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;
                    return adoptPetApplication;
                },
                new { Id = Id },
                splitOn: "IsAdoptDeleted,IsAccountDeleted, IsCatDeleted"
            );

            return result.FirstOrDefault();
        }
    }


    public Task<int> UpdateAsync(AdoptPetApplication entity)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<AdoptPetApplication>> GetAllApplicationsByAdopterAsync(
      Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "a.Id", "a.MeetingDate", "a.ReasonReject", "a.Status", "a.IsFinalized", "a.Description",
            "a.CreatedDate", "a.IsDeleted AS IsAdoptDeleted",
            "acc.Id AS AccountId", "acc.FirstName", "acc.LastName", "acc.Email", "acc.PhoneNumber", "acc.IsDeleted AS IsAccountDeleted",
            "c.Id AS CatId", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Weight", "c.Color", "c.Description AS CatDescription",
            "c.Sterilization", "c.IsDeleted AS IsCatDeleted",
            "ic.ImageUrl", "ic.PublicImageId"
        };

            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString} 
            FROM AdoptPetApplications a
            JOIN Accounts acc ON acc.Id = a.AccountId
            JOIN Cats c ON c.Id = a.CatId
            LEFT JOIN (
                SELECT ic.CatId, ic.ImageUrl, ic.PublicImageId
                FROM ImageCats ic
                WHERE ic.Id = (
                    SELECT TOP 1 ic2.Id 
                    FROM ImageCats ic2 
                    WHERE ic2.CatId = ic.CatId 
                    ORDER BY ic2.CreatedDate
                )
            ) AS ic ON c.Id = ic.CatId
            WHERE a.IsDeleted = 0 AND a.AccountId = @AccountId");

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

            // Filter by status if specified
            if (filterParams?.Status.HasValue == true)
            {
                queryBuilder.Append(" AND a.Status = @Status");
                totalCountQuery.Append(" AND a.Status = @Status");
                parameters.Add("Status", filterParams.Status);
            }

            // Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY a.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = await connection.QueryAsync<AdoptPetApplication, Account, Cat, ImageCat, AdoptPetApplication>(
                paginatedQuery,
                (adoptPetApplication, account, cat, imageCat) =>
                {
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;

                    // Khởi tạo danh sách ảnh nếu chưa có
                    cat.ImageCats = cat.ImageCats ?? new List<ImageCat>();

                    // Nếu có ảnh, thêm vào danh sách
                    if (imageCat != null)
                    {
                        cat.ImageCats.Add(new ImageCat
                        {
                            ImageUrl = imageCat.ImageUrl,
                            PublicImageId = imageCat.PublicImageId
                        });
                    }

                    return adoptPetApplication;
                },
                parameters,
                splitOn: "AccountId,CatId,ImageUrl" // Điều chỉnh splitOn theo thứ tự của cột trong SELECT
            );

            return new PagedResult<AdoptPetApplication>(items.ToList(), pageIndex, pageSize, totalCount, totalPages);
        }
    }

    public async Task<PagedResult<AdoptPetApplication>> GetAllApplicationByStaffAsync(
     Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Define valid columns to prevent SQL injection
            var validColumns = new HashSet<string>
        {
            "a.Id", "a.MeetingDate", "a.ReasonReject", "a.Status", "a.IsFinalized", "a.Description",
            "a.CreatedDate", "a.IsDeleted AS IsAdoptDeleted",
            "acc.Id AS AccountId", "acc.FirstName", "acc.LastName", "acc.Email", "acc.PhoneNumber", "acc.IsDeleted AS IsAccountDeleted",
            "c.Id AS CatId", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Weight", "c.Color", "c.Description AS CatDescription",
            "c.Sterilization", "c.IsDeleted AS IsCatDeleted",
            "ic.ImageUrl", "ic.PublicImageId"
        };

            // Select valid columns or default to all if none specified
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray() ?? validColumns.ToArray();
            var selectedColumnsString = string.Join(", ", columns);

            // Base query for selecting applications
            var baseQuery = $@"
            SELECT {selectedColumnsString} 
            FROM AdoptPetApplications a
            JOIN Accounts acc ON acc.Id = a.AccountId
            JOIN Cats c ON c.Id = a.CatId
            JOIN Branchs b ON b.Id = c.BranchId
           LEFT JOIN (
                SELECT ic.CatId, ic.ImageUrl, ic.PublicImageId
                FROM ImageCats ic
                WHERE ic.Id = (
                    SELECT TOP 1 ic2.Id 
                    FROM ImageCats ic2 
                    WHERE ic2.CatId = ic.CatId 
                    ORDER BY ic2.CreatedDate
                )
            ) AS ic ON c.Id = ic.CatId
            WHERE a.IsDeleted = 0 AND b.AccountId = @AccountId";

            var parameters = new DynamicParameters();
            parameters.Add("AccountId", accountId);

            // Add filtering by status if specified
            if (filterParams?.Status.HasValue == true)
            {
                baseQuery += " AND a.Status = @Status";
                parameters.Add("Status", filterParams.Status);
            }

            // Calculate pagination values
            pageIndex = Math.Max(pageIndex, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            // Query for total count
            var totalCountQuery = $@"
            SELECT COUNT(1) 
            FROM AdoptPetApplications a
            JOIN Accounts acc ON acc.Id = a.AccountId
            JOIN Cats c ON c.Id = a.CatId
            JOIN Branchs b ON b.Id = c.BranchId
            WHERE a.IsDeleted = 0 AND b.AccountId = @AccountId";

            // Retrieve total count and calculate pages
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery, parameters);
            var totalPages = Math.Ceiling(totalCount / (double)pageSize);

            // Final paginated query
            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{baseQuery} ORDER BY a.CreatedDate {(filterParams.IsAscCreatedDate ? "ASC" : "DESC")} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            // Execute paginated query and map results
            var items = (await connection.QueryAsync<AdoptPetApplication, Account, Cat, ImageCat, AdoptPetApplication>(
                paginatedQuery,
                (adoptPetApplication, account, cat, imageCat) =>
                {
                    cat.ImageCats = cat.ImageCats ?? new List<ImageCat>();
                    if (imageCat != null)
                    {
                        cat.ImageCats.Add(imageCat);
                    }
                    adoptPetApplication.Account = account;
                    adoptPetApplication.Cat = cat;
                    return adoptPetApplication;
                },
                parameters,
                splitOn: "AccountId,CatId,ImageUrl"
            )).ToList();

            return new PagedResult<AdoptPetApplication>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }



    public async Task<IEnumerable<AdoptPetApplication>> GetAllApplicationsByCatId(Guid adoptId, Guid catId)
    {
        var sql = @"
        SELECT
            a.Id, a.MeetingDate, a.ReasonReject, a.Status, a.IsFinalized, a.Description, a.CreatedDate, a.AccountId, a.CatId, a.IsDeleted as IsAdoptDeleted,
            acc.Id, acc.FirstName, acc.LastName, acc.Email, acc.PhoneNumber, acc.IsDeleted as IsAccountDeleted,
            c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Weight, c.Color, c.Description
        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        WHERE a.CatId = @CatId AND a.Status != -1 AND a.Id != @AdoptId";

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
                new
                {
                    CatId = catId,
                    AdoptId = adoptId
                },
                splitOn: "IsAdoptDeleted,IsAccountDeleted"
            );

            return result;
        }
    }

    public async Task<int> CountAllAdoptApplications()
    {
        var sql = "SELECT COUNT(*) FROM AdoptPetApplications";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<int>(sql);
            return result;
        }
    }
}

