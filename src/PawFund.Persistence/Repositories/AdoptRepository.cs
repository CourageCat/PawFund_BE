using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class AdoptRepository(ApplicationDbContext context) : RepositoryBase<AdoptPetApplication, Guid>(context), IAdoptRepository
{
}

