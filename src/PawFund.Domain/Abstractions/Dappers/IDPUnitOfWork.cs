using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Domain.Abstractions.Dappers;

public interface IDPUnitOfWork
{
    IAccountRepository AccountRepositories { get; }
    IAdoptRepository AdoptRepositories { get; }
    ICatRepository CatRepositories { get; }
    IBranchRepository BranchRepositories { get; }
    IEventActivityRepository EventActivityRepositories { get; }
    IEventRepository EventRepository { get; }
    IHistoryCat HistoryCatRepository { get; }
    IRoleUser RoleUserRepository { get; }
    IVolunteerApplication VolunteerApplicationRepository { get; }
    IVolunteerApplicationDetail VolunteerApplicationDetailRepository { get; }
}
