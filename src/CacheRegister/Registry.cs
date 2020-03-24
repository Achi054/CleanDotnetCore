using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Options;
using OrderApi.Services;
using Repository.Contracts;
using Repository.EntityFramework.Cached;
using StackExchange.Redis;

namespace CacheRegister
{
    public static class Registry
    {
        public static void AddCacheDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Decorate<IOrderRepository<Domain.EFCoreEntities.Order>, CachedOrderRepository>();

            //Redis Cache registry
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
                return;

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString));
            services.AddStackExchangeRedisCache(setupAction => setupAction.Configuration = redisCacheSettings.ConnectionString);

            services.AddSingleton(redisCacheSettings);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
