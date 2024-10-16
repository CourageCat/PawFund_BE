
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Accounts;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
{
    Task<bool> EmailExistSystem(string email);
    Task<Account> GetByEmailAsync(string email);

    Task<List<Account>> GetListUser();

    Task<PagedResult<Account>> GetPagedAsync(int pageIndex, int pageSize, Filter.AccountFilter filterParams, string[] selectedColumns);
}
