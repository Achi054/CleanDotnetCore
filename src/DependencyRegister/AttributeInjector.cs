using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyRegister
{
    public static class AttributeInjector
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            RegisterWithAttribute(ref services, typeof(InjectableTransientAttribute));
            RegisterWithAttribute(ref services, typeof(InjectableScopedAttribute));
            RegisterWithAttribute(ref services, typeof(InjectableSingletonAttribute));
        }

        private static void RegisterWithAttribute(ref IServiceCollection services, Type type)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var allTypes = assembly.GetTypes();

                var classesWithAttribute = allTypes.Where(x => x.IsClass && x.CustomAttributes.Any(y => y.AttributeType == type));

                foreach (var implementationType in classesWithAttribute)
                {
                    var interfaceType = allTypes.FirstOrDefault(x => x.IsInterface && x.Name.Substring(1) == implementationType.Name);

                    if (interfaceType == null) throw new Exception($"Failed to resolve interface for class {implementationType.Name}, unable to inject dependency!");

                    _ = (type switch
                    {
                        { Name: nameof(InjectableTransientAttribute) } => services.AddTransient(interfaceType, implementationType),
                        { Name: nameof(InjectableScopedAttribute) } => services.AddScoped(interfaceType, implementationType),
                        { Name: nameof(InjectableSingletonAttribute) } => services.AddSingleton(interfaceType, implementationType),
                        _ => throw new ArgumentException(message: "Invalid register type", paramName: nameof(interfaceType))
                    });
                }
            }
        }
    }
}
