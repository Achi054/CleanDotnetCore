using Domain.EFCoreEntities;
using MediatR;

namespace OrderService.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public CreateOrderCommand(Order order) => Order = order;

        public Order Order { get; set; }
    }
}
