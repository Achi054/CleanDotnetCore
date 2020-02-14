using Domain.EFCoreEntities;
using MediatR;

namespace OrderService.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public GetOrderByIdQuery(int id) => OrderId = id;

        public int OrderId { get; set; }
    }
}
