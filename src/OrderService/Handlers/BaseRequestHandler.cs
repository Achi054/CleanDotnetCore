using Domain;
using Repository.Contracts;

namespace OrderService.Handlers
{
    public class BaseRequestHandler
    {
        protected readonly IeComRepository<Order> _eComRepository;

        public BaseRequestHandler(IeComRepository<Order> eComRepository)
        {
            _eComRepository = eComRepository;
        }
    }
}
