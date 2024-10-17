using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IDonationRepository : IGenericRepository<Donation>
{
    Task<Donation> GetLatestDonationAsync();
}
