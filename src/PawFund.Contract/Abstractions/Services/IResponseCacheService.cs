namespace PawFund.Contract.Abstractions.Services;

public interface IResponseCacheService
{
    Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
    Task<string> GetCacheResponseAsync(string cacheKey);
    Task DeleteCacheResponseAsync(string cacheKey);
}