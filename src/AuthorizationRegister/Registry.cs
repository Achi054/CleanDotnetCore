using AuthorizationRegister.RequirementHandlers;
using AuthorizationRegister.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationRegister
{
    public static class AuthorizationExtensions
    {
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(configure =>
            {
                //configure.AddPolicy("CanDelete", configurePolicy => configurePolicy.RequireClaim("order.delete", "true"));
                configure.AddPolicy("WorkingInCompany", policy =>
                {
                    policy.AddRequirements(new DomainNameRequirement("company.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, DomainNameRequirementHandler>();
        }
    }
}
