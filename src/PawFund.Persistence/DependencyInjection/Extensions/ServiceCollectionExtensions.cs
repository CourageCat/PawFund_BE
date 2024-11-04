using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Persistence.DependencyInjection.Options;
using PawFund.Persistence.Repositories;

namespace PawFund.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                    sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IEFUnitOfWork), typeof(EFUnitOfWork))
            .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddTransient<IAccountRepository, AccountRepository>()
            .AddTransient<IAdoptRepository, AdoptRepository>()
            .AddTransient<ICatRepository, CatRepository>()
            .AddTransient<IBranchRepository, BranchRepository>()
            .AddTransient<IEventActivityRepository, EventActivityRepository>()
            .AddTransient<IEventRepository, EventRepository>()
            .AddTransient<IHistoryCatRepository, HistoryCatRepository>()
            .AddTransient<IVolunteerApplicationDetail, VolunteerApplicationDetail>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IDonationRepository, DonationRepository>()
            .AddTransient<IChatHistoryRepository, ChatHistoryRepository>()
            .AddTransient<IMessageRepository, MessageRepository>();

    }

    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions
        (this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
