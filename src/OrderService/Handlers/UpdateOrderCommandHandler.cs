using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using OrderService.Commands;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class UpdateOrderCommandHandler : BaseRequestHandler, IRequestHandler<UpdateOrderCommand, Order>
    {
        public UpdateOrderCommandHandler(IeComRepository<Order> eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await _eComRepository.Order.UpdateOrder(request.Order);

            return request.Order;
        }
    }
}
