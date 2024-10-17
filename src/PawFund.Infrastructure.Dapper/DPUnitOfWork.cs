using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Infrastructure.Dapper.Repositories;

namespace PawFund.Infrastructure.Dapper;

public class DPUnitOfWork : IDPUnitOfWork
{
    public DPUnitOfWork(IAccountRepository accountRepositories, IAdoptRepository adoptRepositories, ICatRepository catRepositories, IBranchRepository branchRepositories, IEventActivityRepository eventActivityRepositories, IEventRepository eventRepository, IHistoryCatRepository historyCatRepository, IRoleUser roleUserRepository, IVolunteerApplicationDetail volunteerApplicationDetailRepository)
    {
        AccountRepositories = accountRepositories;
        AdoptRepositories = adoptRepositories;
        CatRepositories = catRepositories;
        BranchRepositories = branchRepositories;
        EventActivityRepositories = eventActivityRepositories;
        EventRepository = eventRepository;
        HistoryCatRepository = historyCatRepository;
        RoleUserRepository = roleUserRepository;
        VolunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
    }

    public IAccountRepository AccountRepositories { get; }
    public IAdoptRepository AdoptRepositories { get; }
    public ICatRepository CatRepositories { get; }

    public IBranchRepository BranchRepositories { get; }

    public IEventActivityRepository EventActivityRepositories { get; }

    public IEventRepository EventRepository { get; }

    public IHistoryCatRepository HistoryCatRepository{ get; }

    public IRoleUser RoleUserRepository { get; }

    public IVolunteerApplicationDetail VolunteerApplicationDetailRepository { get; }

    public IHistoryCatRepository HistoryCatRepositories => throw new NotImplementedException();
}
