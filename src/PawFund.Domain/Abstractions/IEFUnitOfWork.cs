namespace PawFund.Domain.Abstractions;

public interface IEFUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}