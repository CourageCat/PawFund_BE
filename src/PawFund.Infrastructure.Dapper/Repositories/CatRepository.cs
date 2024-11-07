using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Text;
using static PawFund.Contract.Services.Cats.Filter;

namespace PawFund.Infrastructure.Dapper.Repositories;
public class CatRepository : ICatRepository
{
    private readonly IConfiguration _configuration;

    public CatRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Cat entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Cat entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Cat>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Cat>? GetByIdAsync(Guid Id)
    {

        var sql = @"
        SELECT c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Size, c.Color, c.Description
        FROM Cats c
        WHERE c.Id = @Id";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Cat>(
                sql,
                new { Id = Id }
                );

            return result.FirstOrDefault();
        }
    }

    public Task<PagedResult<Cat>> GetPagedAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<Cat>> GetAllCatsForAdoptionAsync(
      int pageIndex,
      int pageSize,
      CatAdoptFilter filterParams,
      string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var validColumns = new HashSet<string>
        {
            "c.Id", "c.Sex", "c.Name", "c.Age", "c.Breed", "c.Color", "c.Description", "c.Sterilization", "c.BranchId", "c.IsDeleted",
            "ImageCat.ImageUrl", "ImageCat.PublicImageId"
        };
            
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : string.Join(", ", validColumns);

            var queryBuilder = new StringBuilder($@"
            SELECT {selectedColumnsString} 
            FROM Cats c
            LEFT JOIN (
                SELECT ic.CatId, ic.ImageUrl, ic.PublicImageId
                FROM ImageCats ic
                WHERE ic.Id = (
                    SELECT TOP 1 ic2.Id 
                    FROM ImageCats ic2 
                    WHERE ic2.CatId = ic.CatId 
                    ORDER BY ic2.CreatedDate
                )
            ) AS ImageCat ON c.Id = ImageCat.CatId
            WHERE 1=1");

            var parameters = new DynamicParameters();

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize > 100 ? 100 : pageSize;

            var totalCountQuery = new StringBuilder($@"
            SELECT COUNT(1) 
            FROM Cats c
            LEFT JOIN (
                SELECT ic.CatId
                FROM ImageCats ic
                WHERE ic.Id = (
                    SELECT TOP 1 ic2.Id 
                    FROM ImageCats ic2 
                    WHERE ic2.CatId = ic.CatId
                    ORDER BY ic2.CreatedDate
                )
            ) AS ImageCat ON c.Id = ImageCat.CatId
            WHERE 1=1");

            // Filter by Deleted
            if (filterParams?.IsDeleted.HasValue == true)
            {
                queryBuilder.Append(" AND c.IsDeleted = @IsDeleted");
                totalCountQuery.Append(" AND c.IsDeleted = @IsDeleted");
                parameters.Add("IsDeleted", filterParams.IsDeleted.Value);
            }

            // Filter by BranchId
            if (filterParams?.BranchId.HasValue == true)
            {
                queryBuilder.Append(" AND c.BranchId = BranchId");
                totalCountQuery.Append(" AND c.BranchId = @BranchId");
                parameters.Add("BranchId", filterParams.BranchId.Value);
            }

            // Filter by Name
            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                queryBuilder.Append(" AND c.Name LIKE @Name");
                totalCountQuery.Append(" AND c.Name LIKE @Name");
                parameters.Add("Name", $"%{filterParams.Name}%");
            }

            // Filter by Color
            if (!string.IsNullOrEmpty(filterParams.Color))
            {
                queryBuilder.Append(" AND c.Color LIKE @Color");
                totalCountQuery.Append(" AND c.Color LIKE @Color");
                parameters.Add("Color", $"%{filterParams.Color}%");
            }

            // Filter by Cat Sex
            if (filterParams.CatSex.HasValue)
            {
                queryBuilder.Append(" AND c.Sex = @Sex");
                totalCountQuery.Append(" AND c.Sex = @Sex");
                parameters.Add("Sex", filterParams.CatSex.Value);
            }

            // Filter by Sterilization
            if (filterParams.Sterilization.HasValue)
            {
                queryBuilder.Append(" AND c.Sterilization = @Sterilization");
                totalCountQuery.Append(" AND c.Sterilization = @Sterilization");
                parameters.Add("Sterilization", filterParams.Sterilization.Value);
            }

            // Filter by Age
            if (!string.IsNullOrEmpty(filterParams.Age))
            {
                queryBuilder.Append(" AND c.Age = @Age");
                totalCountQuery.Append(" AND c.Age = @Age");
                parameters.Add("Age", filterParams.Age);
            }

            // Count TotalCount
            var totalCount = await connection.ExecuteScalarAsync<int>(totalCountQuery.ToString(), parameters);

            // Count TotalPages
            var totalPages = Math.Ceiling((totalCount / (double)pageSize));

            var offset = (pageIndex - 1) * pageSize;
            var paginatedQuery = $"{queryBuilder} ORDER BY c.CreatedDate OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var items = (await connection.QueryAsync<Cat, ImageCat, Cat>(
                paginatedQuery,
                (cat, imageCat) =>
                {
                    cat.ImageCats = new List<ImageCat>();
                    if (imageCat != null)
                    {
                        cat.ImageCats.Add(imageCat);
                    }
                    return cat;
                },
                parameters,
                splitOn: "ImageUrl"
            )).ToList();

            return new PagedResult<Cat>(items, pageIndex, pageSize, totalCount, totalPages);
        }
    }


    public Task<int> UpdateAsync(Cat entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Cat> GetCatByIdAsync(Guid catId)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var query = @"
        SELECT c.*, ic.ImageUrl, ic.PublicImageId, ic.Id
        FROM Cats c
        LEFT JOIN ImageCats ic ON c.Id = ic.CatId
        WHERE c.Id = @CatId";

            var parameters = new { CatId = catId };

            var catDictionary = new Dictionary<Guid, Cat>();

            await connection.QueryAsync<Cat, ImageCat, Cat>(
                query,
                (cat, imageCat) =>
                {
                    if (!catDictionary.TryGetValue(cat.Id, out var existingCat))
                    {
                        existingCat = cat;
                        existingCat.ImageCats = new List<ImageCat>();
                        catDictionary.Add(existingCat.Id, existingCat);
                    }

                    if (imageCat != null)
                    {
                        existingCat.ImageCats.Add(imageCat);
                    }

                    return existingCat;
                },
                parameters,
                splitOn: "ImageUrl"
            );

            return catDictionary.Values.FirstOrDefault();
        }
    }

}

