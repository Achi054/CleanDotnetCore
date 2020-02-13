using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using OrderService.Commands;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class CreateOrderCommandHandler : BaseRequestHandler, IRequestHandler<CreateOrderCommand, Order>
    {
        public CreateOrderCommandHandler(IeComRepository<Order> eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _eComRepository.Order.CreateOrder(request.Order);

            return request.Order;
        }
    }
}
