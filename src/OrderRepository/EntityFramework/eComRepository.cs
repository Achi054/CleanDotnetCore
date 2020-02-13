using System.Threading.Tasks;
using DependencyRegister;
using Domain;
using Repository.Contracts;
using Repository.EntityFramework.Context;

namespace Repository.EntityFramework
{
    [InjectableScoped]
    public class eComRepository : IeComRepository<Order>
    {
        private readonly eComContext _context;
        private readonly IOrderRepository<Order> _orderRepository;

        public eComRepository(eComContext context, IOrderRepository<Order> orderRepository)
            => (_context, _orderRepository) = (context, orderRepository);

        public IOrderRepository<Order> Order => _orderRepository;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
