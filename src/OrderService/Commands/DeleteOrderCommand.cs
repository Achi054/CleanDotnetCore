using MediatR;

namespace OrderService.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public DeleteOrderCommand(int id) => OrderId = id;

        public int OrderId { get; set; }
    }
}
