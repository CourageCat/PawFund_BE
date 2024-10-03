using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Domain.Abstractions.Dappers;

public interface IDPUnitOfWork
{
    IAccountRepository AccountRepositories { get; }
}
