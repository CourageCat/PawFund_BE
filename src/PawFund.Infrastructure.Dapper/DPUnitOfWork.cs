using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Infrastructure.Dapper.Repositories;

namespace PawFund.Infrastructure.Dapper;

public class DPUnitOfWork : IDPUnitOfWork
{
    public DPUnitOfWork(IAccountRepository accountRepositories, IAdoptRepository adoptRepository, ICatRepository catRepository)
    {
        AccountRepositories = accountRepositories;
        AdoptRepositories = adoptRepository;
        CatRepositories = catRepository;
    }

    public IAccountRepository AccountRepositories { get; }
    public IAdoptRepository AdoptRepositories { get; }
    public ICatRepository CatRepositories { get; }
}
