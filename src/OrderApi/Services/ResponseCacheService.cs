using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace OrderApi.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        => _distributedCache = distributedCache;

        public async Task CacheResponseAsync(string cacheKey, object cacheObject, TimeSpan timeToLive)
        {
            if (cacheObject != null)
                return;

            var serializedObject = JsonSerializer.Serialize(cacheObject);

            await _distributedCache.SetStringAsync(cacheKey, serializedObject, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
            => await _distributedCache.GetStringAsync(cacheKey);
    }
}
