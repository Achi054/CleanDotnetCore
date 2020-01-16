using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using OrderService.Commands;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class DeleteOrderCommandHandler : BaseRequestHandler, IRequestHandler<DeleteOrderCommand, Order>
    {
        public DeleteOrderCommandHandler(IeComRepository eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _eComRepository.Order.DeleteOrder(request.Order);

            return request.Order;
        }
    }
}
