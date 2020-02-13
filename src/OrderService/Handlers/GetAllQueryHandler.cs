using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using OrderService.Queries;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class GetAllQueryHandler : BaseRequestHandler, IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
    {
        public GetAllQueryHandler(IeComRepository<Order> eComRepository)
            : base(eComRepository)
        {
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            => await _eComRepository.Order.GetAllOrder();
    }
}
