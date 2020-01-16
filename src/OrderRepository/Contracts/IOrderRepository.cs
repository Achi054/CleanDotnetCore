using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Repository.Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllOrder();

        Task<Order> GetOrderById(int id);

        Task CreateOrder(Order order);

        Task DeleteOrder(Order order);

        Task UpdateOrder(Order order);
    }
}
