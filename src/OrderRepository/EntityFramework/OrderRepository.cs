using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyRegister;
using Domain.EFCoreEntities;
using Repository.Contracts;
using Repository.EntityFramework;
using Repository.EntityFramework.Context;

namespace OrderRepository.EntityFramework
{
    [InjectableScoped]
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository<Order>
    {
        public OrderRepository(eComContext context)
            : base(context) => context.Database.EnsureCreated();

        public async Task CreateOrder(Order order) => await Create(order);

        public async Task DeleteOrder(int id)
        {
            var orders = await FindAllByCondition(x => x.Id == id);
            await Delete(orders.SingleOrDefault());
        }

        public async Task<IEnumerable<Order>> GetAllOrder() => await FindAll();

        public async Task<Order> GetOrderById(int id)
        {
            var orders = await FindAllByCondition(x => x.Id == id);
            return orders.SingleOrDefault();
        }

        public async Task UpdateOrder(Order order) => await Update(order);
    }
}
