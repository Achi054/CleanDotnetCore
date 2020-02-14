using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmonaut;
using DependencyRegister;
using Domain.CosmosDb.Entities;
using Repository.Contracts;

namespace Repository.CosmosDb
{
    [InjectableScoped]
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository<Order>
    {
        private ICosmosStore<Order> _context;

        public OrderRepository(ICosmosStore<Order> cosmosStore)
            : base(cosmosStore) => _context = cosmosStore;

        public async Task CreateOrder(Order order)
            => await Create(order);

        public async Task DeleteOrder(int id)
        {
            var orders = await FindAllByCondition(x => x.Id == id);
            await Delete(orders.FirstOrDefault());
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
            => await FindAll();

        public async Task<Order> GetOrderById(int id)
        {
            var orders = await FindAllByCondition(x => x.Id == id);
            return orders.FirstOrDefault();
        }

        public async Task UpdateOrder(Order order)
            => await Update(order);
    }
}
