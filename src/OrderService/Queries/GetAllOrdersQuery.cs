using System.Collections.Generic;
using Domain.EFCoreEntities;
using MediatR;

namespace OrderService.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}
