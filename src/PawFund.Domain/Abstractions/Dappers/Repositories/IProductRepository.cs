using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Products;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IProductRepository : IGenericRepository<Domain.Entities.Product>
{
    Task<PagedResult<Product>> GetPagedAsync(int pageIndex, int pageSize, Filter.ProductFilter filterParams, string[] selectedColumns);
}