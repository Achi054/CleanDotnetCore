using System.Threading.Tasks;
using DependencyRegister;
using Repository.Context;
using Repository.Contracts;

namespace Repository
{
    [InjectableScoped]
    public class eComRepository : IeComRepository
    {
        private readonly eComContext _context;
        private readonly IOrderRepository _orderRepository;

        public eComRepository(eComContext context, IOrderRepository orderRepository)
            => (_context, _orderRepository) = (context, orderRepository);

        public IOrderRepository Order => _orderRepository;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
