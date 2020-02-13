using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IOrderRepository<T> : IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllOrder();

        Task<T> GetOrderById(int id);

        Task CreateOrder(T order);

        Task DeleteOrder(int id);

        Task UpdateOrder(T order);
    }
}
