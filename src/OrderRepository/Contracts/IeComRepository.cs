using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IeComRepository<T> where T : class
    {
        IOrderRepository<T> Order { get; }

        Task SaveAsync();
    }
}
