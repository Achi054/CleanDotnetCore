using Microsoft.Extensions.DependencyInjection;
using Repository.Cached;
using Repository.Contracts;

namespace CacheRegister
{
    public static class Registry
    {
        public static void AddCacheDependencies(this IServiceCollection services)
            => services.Decorate<IOrderRepository, CachedOrderRepository>();
    }
}
