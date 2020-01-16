using Repository.Contracts;

namespace OrderService.Handlers
{
    public class BaseRequestHandler
    {
        protected readonly IeComRepository _eComRepository;

        public BaseRequestHandler(IeComRepository eComRepository)
        {
            _eComRepository = eComRepository;
        }
    }
}
