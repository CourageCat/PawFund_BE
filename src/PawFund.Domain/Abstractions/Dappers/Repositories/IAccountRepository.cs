
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Products;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
{
    Task<bool> EmailExistSystem(string email);
    Task<Account> GetByEmailAsync(string email);
    //Task<PagedResult<Account>> GetPagedAsync(int pageIndex, int pageSize, Filter.ProductFilter filterParams, string[] selectedColumns)
}
