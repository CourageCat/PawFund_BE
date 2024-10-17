using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Infrastructure.Dapper;

public class DPUnitOfWork : IDPUnitOfWork
{
    public DPUnitOfWork(IAccountRepository accountRepositories, IAdoptRepository adoptRepositories, ICatRepository catRepositories, IBranchRepository branchRepositories, IEventActivityRepository eventActivityRepositories, IEventRepository eventRepository, IHistoryCat historyCatRepository, IRoleUser roleUserRepository, IVolunteerApplicationDetail volunteerApplicationDetailRepository, IDonationRepository donationRepository)
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
        DonationRepository = donationRepository;
    }

    public IAccountRepository AccountRepositories { get; }
    public IAdoptRepository AdoptRepositories { get; }
    public ICatRepository CatRepositories { get; }

    public IBranchRepository BranchRepositories { get; }

    public IEventActivityRepository EventActivityRepositories { get; }

    public IEventRepository EventRepository { get; }

    public IHistoryCat HistoryCatRepository { get; }

    public IRoleUser RoleUserRepository { get; }

    public IVolunteerApplicationDetail VolunteerApplicationDetailRepository { get; }

    public IDonationRepository DonationRepository { get; }
}
