using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class DonationRepository(ApplicationDbContext context) : RepositoryBase<Donation, Guid>(context), IDonationRepository
{
}