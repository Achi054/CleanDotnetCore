using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IeComRepository
    {
        IOrderRepository Order { get; }

        Task SaveAsync();
    }
}
