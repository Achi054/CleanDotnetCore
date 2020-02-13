using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contracts;
using Repository.EntityFramework.Cached;

namespace CacheRegister
{
    public static class Registry
    {
        public static void AddCacheDependencies(this IServiceCollection services)
            => services.Decorate<IOrderRepository<Order>, CachedOrderRepository>();
    }
}
