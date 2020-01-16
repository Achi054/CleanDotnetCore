using Domain;
using MediatR;

namespace OrderService.Commands
{
    public class DeleteOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
