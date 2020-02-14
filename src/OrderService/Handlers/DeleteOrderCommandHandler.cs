using System.Threading;
using System.Threading.Tasks;
using Domain.EFCoreEntities;
using MediatR;
using OrderService.Commands;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class DeleteOrderCommandHandler : BaseRequestHandler, IRequestHandler<DeleteOrderCommand>
    {
        public DeleteOrderCommandHandler(IeComRepository<Order> eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _eComRepository.Order.DeleteOrder(request.OrderId);
            return Unit.Value;
        }
    }
}
