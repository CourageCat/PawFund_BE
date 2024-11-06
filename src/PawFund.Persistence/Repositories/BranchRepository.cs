using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class BranchRepository(ApplicationDbContext context) : RepositoryBase<Branch, Guid>(context), IBranchRepository
{
}