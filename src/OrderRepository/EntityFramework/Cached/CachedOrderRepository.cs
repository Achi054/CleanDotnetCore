using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Repository.Contracts;
using Repository.EntityFramework.Context;

namespace Repository.EntityFramework.Cached
{
    public class CachedOrderRepository : RepositoryBase<Order>, IOrderRepository<Order>
    {
        private readonly IOrderRepository<Order> _repository;
        private ConcurrentDictionary<int, Order> cachedOrders = new ConcurrentDictionary<int, Order>();

        public CachedOrderRepository(eComContext context, IOrderRepository<Order> orderRepository)
            : base(context) => _repository = orderRepository;

        public async Task CreateOrder(Order order) => await _repository.CreateOrder(order);

        public async Task DeleteOrder(int id)
        {
            await _repository.DeleteOrder(id);
            cachedOrders.TryRemove(id, out _);
        }

        public async Task<IEnumerable<Order>> GetAllOrder() => await _repository.GetAllOrder();


        public async Task<Order> GetOrderById(int id)
        {
            if (cachedOrders.ContainsKey(id))
                return await Task.FromResult(cachedOrders[id]);

            var order = await _repository.GetOrderById(id);
            cachedOrders.TryAdd(id, order);

            return order;
        }

        public async Task UpdateOrder(Order order)
        {
            if (cachedOrders.ContainsKey(order.Id))
                cachedOrders.TryUpdate(order.Id, order, cachedOrders[order.Id]);

            await _repository.UpdateOrder(order);
        }
    }
}
