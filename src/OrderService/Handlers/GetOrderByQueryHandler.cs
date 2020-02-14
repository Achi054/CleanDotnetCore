using System.Threading;
using System.Threading.Tasks;
using Domain.EFCoreEntities;
using MediatR;
using OrderService.Queries;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class GetOrderByQueryHandler : BaseRequestHandler, IRequestHandler<GetOrderByIdQuery, Order>
    {
        public GetOrderByQueryHandler(IeComRepository<Order> eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            => await _eComRepository.Order.GetOrderById(request.OrderId);
    }
}
