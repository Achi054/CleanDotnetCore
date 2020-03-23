using System;
using System.Threading.Tasks;

namespace OrderApi.Services
{
    public interface IResponseCacheService
    {
        Task<string> GetCacheResponseAsync(string cacheKey);

        Task CacheResponseAsync(string cacheKey, object cacheObject, TimeSpan timeToLive);
    }
}
