using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Handlers;

namespace MediatrRegister
{
    public static class Registry
    {
        public static void AddMediater(this IServiceCollection services)
            => services.AddMediatR(typeof(UpdateOrderCommandHandler).Assembly);
    }
}
