using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyRegister
{
    public static class CosmosDbRegister
    {
        public static void AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDbSetting = new CosmosStoreSettings(
                configuration["CosmosSettings:DatabaseName"],
                configuration["CosmosSettings:AccountUri"],
                configuration["CosmosSettings:AccountKey"]);

            services.AddCosmosStore<Order>(cosmosDbSetting);
        }
    }
}
