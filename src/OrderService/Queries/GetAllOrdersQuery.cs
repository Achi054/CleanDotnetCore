using System.Collections.Generic;
using Domain;
using MediatR;

namespace OrderService.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}
