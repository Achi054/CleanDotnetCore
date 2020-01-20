using System.Collections.Generic;
using System.Threading.Tasks;
using DependencyRegister;
using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Context;
using Repository.Contracts;

namespace OrderRepository
{
    [InjectableScoped]
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(eComContext context)
            : base(context) { }

        public async Task CreateOrder(Order order) => await Task.Run(() => Create(order));

        public async Task DeleteOrder(int id)
        {
            var order = await FindAllByCondition(x => x.Id == id).FirstOrDefaultAsync();
            await Task.Run(() => Delete(order));
        }

        public async Task<IEnumerable<Order>> GetAllOrder() => await FindAll().ToListAsync();

        public async Task<Order> GetOrderById(int id)
            => await FindAllByCondition(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateOrder(Order order) => await Task.Run(() => Update(order));
    }
}
