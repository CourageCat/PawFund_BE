
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
{
    Task<bool> EmailExistSystem(string email);
    Task<Account> GetByEmailAsync(string email);
}
