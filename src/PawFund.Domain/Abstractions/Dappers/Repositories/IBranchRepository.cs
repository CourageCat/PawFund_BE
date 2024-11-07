using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Branchs.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IBranchRepository : IGenericRepository<Branch>
{
    Task<PagedResult<Branch>> GetPagedAsync(int pageIndex, int pageSize, BranchFilter filterParams, string[] selectedColumns);

    Task<List<Guid>> GetAllBranchByAccountId(Guid id);
}

