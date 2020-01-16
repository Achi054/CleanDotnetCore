using Domain;
using MediatR;

namespace OrderService.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int OrderId { get; set; }
    }
}
