using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Cats.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;
public interface ICatRepository : IGenericRepository<Cat>
{
    Task<PagedResult<Cat>> GetAllCatsForAdoptionAsync(
        int pageIndex,
        int pageSize,
        CatAdoptFilter filterParams,
        string[] selectedColumns);
}

