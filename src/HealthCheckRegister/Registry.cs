using Microsoft.Extensions.DependencyInjection;
using Repository.EntityFramework.Context;

namespace HealthCheckRegister
{
    public static class Registry
    {
        public static void AddHealthChecker(this IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<eComContext>()
                    .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
