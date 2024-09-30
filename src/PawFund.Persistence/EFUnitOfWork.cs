using PawFund.Domain.Abstractions;

namespace PawFund.Persistence;

public class EFUnitOfWork : IEFUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EFUnitOfWork(ApplicationDbContext context)
        => _context = context;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
     =>   await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();
}
