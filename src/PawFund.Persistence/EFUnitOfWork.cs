using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;

namespace PawFund.Persistence;

public class EFUnitOfWork : IEFUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAccountRepository _accountRepository;
    private readonly IAdoptRepository _adoptRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICatRepository _catRepository;
    private readonly IChatHistoryRepository _chatHistoryRepository;
    private readonly IDonationRepository _donationRepository;
    private readonly IEventActivityRepository _eventActivityRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IHistoryCatRepository _historyCatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IProductRepository _productRepository;
    private readonly IVolunteerApplicationDetail _volunteerApplicationDetailRepository;

    public EFUnitOfWork(ApplicationDbContext context, IAccountRepository accountRepository)
    {
        _context = context;
        _accountRepository = accountRepository;
    }
     
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
     =>   await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();

    public IAccountRepository AccountRepository => _accountRepository;
    public IAdoptRepository AdoptRepository => _adoptRepository;
    public IBranchRepository BranchRepository => _branchRepository;
    public ICatRepository CatRepository => _catRepository;
    public IChatHistoryRepository ChatHistoryRepository => _chatHistoryRepository;
    public IDonationRepository DonationRepository => _donationRepository;
    public IEventActivityRepository EventActivityRepository => _eventActivityRepository;
    public IEventRepository EventRepository => _eventRepository;
    public IHistoryCatRepository HistoryCatRepository => _historyCatRepository;
    public IMessageRepository MessageRepository => _messageRepository;
    public IProductRepository ProductRepository => _productRepository;
    public IVolunteerApplicationDetail VolunteerApplicationDetail => _volunteerApplicationDetailRepository;
}
