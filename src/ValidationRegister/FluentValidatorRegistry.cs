using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.PipelineBehaviors;
using OrderService.Validators;

namespace ValidationRegister
{
    public static class FluentValidatorRegistry
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(GetOrderByIdQueryValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
