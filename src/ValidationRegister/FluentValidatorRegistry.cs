using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Validators;

namespace ValidationRegister
{
    public static class FluentValidatorRegistry
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(GetOrderByIdQueryValidator).Assembly);

            services.Scan(scan => scan
                .FromAssemblyOf<GetOrderByIdQueryValidator>()
                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsMatchingInterface()
                .WithTransientLifetime()
            );
        }
    }
}
