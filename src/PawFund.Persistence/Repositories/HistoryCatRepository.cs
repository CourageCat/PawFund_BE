using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class HistoryCatRepository(ApplicationDbContext context) : RepositoryBase<HistoryCat, Guid>(context), IHistoryCatRepository
{
}