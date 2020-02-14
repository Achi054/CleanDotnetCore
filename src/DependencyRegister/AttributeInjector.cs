using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyRegister
{
    public static class AttributeInjector
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.Scan(scan =>
                        scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                            // Scan classes with InjectableTransient Attribute 
                            .AddClasses(classes => classes.Where(x => x.IsClass && x.CustomAttributes.Any(y => y.AttributeType == typeof(InjectableTransientAttribute))))
                            .AsImplementedInterfaces()
                            .WithTransientLifetime()
                            // Scan classes with InjectableScoped Attribute 
                            .AddClasses(classes => classes.Where(x => x.IsClass && x.CustomAttributes.Any(y => y.AttributeType == typeof(InjectableScopedAttribute))))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime()
                            // Scan classes with InjectableSingleton Attribute 
                            .AddClasses(classes => classes.Where(x => x.IsClass && x.CustomAttributes.Any(y => y.AttributeType == typeof(InjectableSingletonAttribute))))
                            .AsImplementedInterfaces()
                            .WithSingletonLifetime());
        }
    }
}
