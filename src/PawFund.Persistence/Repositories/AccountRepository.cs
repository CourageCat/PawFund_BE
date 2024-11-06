using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class AccountRepository(ApplicationDbContext context) : RepositoryBase<Account, Guid>(context), IAccountRepository
{
}
