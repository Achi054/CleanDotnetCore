using Domain;
using MediatR;

namespace OrderService.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
