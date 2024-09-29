using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PawFund.Contract.Abstractions;
using PawFund.Contract.Settings;
using PawFund.Infrastructure.Services;
using StackExchange.Redis;

namespace PawFund.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurationRedis
        (this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = new RedisSetting();
        configuration.GetSection(RedisSetting.SectionName).Bind(redisSettings);
        services.AddSingleton<RedisSetting>();
        if (!redisSettings.Enabled) return;
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisSettings.ConnectionString));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.ConnectionString;
        });
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
    }
   
    public static void AddConfigurationService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddTransient<IPasswordHashService, PasswordHashService>();
        services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
    }

    public static void AddConfigurationAppSetting
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationSetting>(configuration.GetSection(AuthenticationSetting.SectionName));
        services.Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName));
    }
}
