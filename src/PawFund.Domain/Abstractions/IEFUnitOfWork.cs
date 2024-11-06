using PawFund.Domain.Abstractions.Repositories;

namespace PawFund.Domain.Abstractions;

public interface IEFUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IAccountRepository AccountRepository { get; }
    IAdoptRepository AdoptRepository { get; }
    IBranchRepository BranchRepository { get; }
    ICatRepository CatRepository { get; }
    IChatHistoryRepository ChatHistoryRepository { get; }
    IDonationRepository DonationRepository { get; }
    IEventActivityRepository EventActivityRepository { get; }
    IEventRepository EventRepository { get; }
    IHistoryCatRepository HistoryCatRepository { get; }
    IMessageRepository MessageRepository { get; }
    IProductRepository ProductRepository { get; }
    IVolunteerApplicationDetail VolunteerApplicationDetail { get; }
    IImageCatRepository ImageCatRepository { get; }
}