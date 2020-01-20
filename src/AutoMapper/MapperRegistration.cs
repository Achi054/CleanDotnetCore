using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperRegister
{
    public static class MapperRegistration
    {
        public static void AddMappers(this IServiceCollection services, Type type)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var allTypes = assembly.GetTypes();

                var classesWithAttribute = allTypes
                    .Where(x => x.IsClass && x.CustomAttributes.Any(y => y.AttributeType == type));

                foreach (var mapperClass in classesWithAttribute)
                {
                    services.AddAutoMapper(mapperClass);
                }
            }
        }
    }
}
