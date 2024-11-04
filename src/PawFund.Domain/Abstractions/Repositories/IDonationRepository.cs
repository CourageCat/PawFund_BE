using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IDonationRepository : IRepositoryBase<Donation, Guid>
{
}
