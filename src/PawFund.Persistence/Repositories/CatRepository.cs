using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;
public class CatRepository(ApplicationDbContext context) : RepositoryBase<Cat, Guid>(context), ICatRepository
{
}

