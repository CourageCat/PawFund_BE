using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Donates;
using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IDonationRepository : IGenericRepository<Donation>
{
    Task<Donation> GetLatestDonationAsync();
    Task<PagedResult<Donation>> GetPagedDonatesAsync(int pageIndex, int pageSize, Filter.DonateFilter filterParams, string[] selectedColumns);
    Task<Donation> GetDonationByOrderIdAsync(long orderId);
}
