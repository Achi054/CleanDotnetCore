using Domain;
using MediatR;

namespace OrderService.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public UpdateOrderCommand(Order order) => Order = order;

        public Order Order { get; set; }
    }
}
