namespace PawFund.Contract.Abstractions;

public interface IResponseCacheService
{
    Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
    Task<string> GetCacheResponseAsync(string cacheKey);
    Task DeleteCacheResponseAsync(string cacheKey);
}