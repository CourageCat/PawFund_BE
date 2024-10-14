using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Products;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System.Text;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<Product>> GetPagedAsync(int pageIndex, int pageSize, Filter.ProductFilter filterParams, string[] selectedColumns)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            // Check column avoid case SQL INJECTION
            var validColumns = new HashSet<string> { "Id", "Name", "Price", "Description" }; // Change column depedency column in database
            var columns = selectedColumns?.Where(c => validColumns.Contains(c)).ToArray();

            // If no select column => GET ALL
            var selectedColumnsString = columns?.Length > 0 ? string.Join(", ", columns) : "*";

            // Concat query
            var queryBuilder = new StringBuilder($"SELECT {selectedColumnsString} FROM Products_VietVy WHERE 1=1");

            var parameters = new DynamicParameters();
            
            // Filter name
            if (!string.IsNullOrEmpty(filterParams?.Name))
            {
                queryBuilder.Append(" AND Name LIKE @Name");
                parameters.Add("Name", $"%{filterParams.Name}%");
            }

            // Filter price
            if (filterParams?.MinPrice.HasValue == true)
            {
                queryBuilder.Append(" AND Price >= @MinPrice");
                parameters.Add("MinPrice", filterParams.MinPrice.Value);
            }

            if (filterParams?.MaxPrice.HasValue == true)
            {
                queryBuilder.Append(" AND Price <= @MaxPrice");
                parameters.Add("MaxPrice", filterParams.MaxPrice.Value);
            }

            return await PagedResult<Product>.CreateAsync(connection, queryBuilder.ToString(), parameters, pageIndex, pageSize);
        }
    }

    public Task<int> UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}
