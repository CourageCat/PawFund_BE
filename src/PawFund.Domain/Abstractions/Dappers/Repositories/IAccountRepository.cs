
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Accounts;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
{
    Task<bool> EmailExistSystemAsync(string email);
    Task<bool>? AccountExistSystemAsync(Guid userId);
    Task<Account> GetByEmailAsync(string email);
    //Task<PagedResult<Account>> GetListUserAsync();
    Task<PagedResult<Account>> GetPagedAsync(int pageIndex, int pageSize, Filter.AccountFilter filterParams, string[] selectedColumns);

    Task<PagedResult<Account>> GetUsersPagedAsync(int pageIndex, int pageSize, Filter.AccountsFilter filterParams, string[] selectedColumns);
    Task<int> CountAllUsers();
    Task<List<Account>> GetAccountDonated();
    Task<Dictionary<int, List<Account>>> FindAllUsersByYear(int year);
    Task<Dictionary<int, List<Account>>> FindAllUsersByYearAndMonth(int year, int month);

}
