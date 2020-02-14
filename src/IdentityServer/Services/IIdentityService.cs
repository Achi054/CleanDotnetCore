using System.Threading.Tasks;
using IdentityServer.Domain;

namespace IdentityServer.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string name, string email, string password);
    }
}
