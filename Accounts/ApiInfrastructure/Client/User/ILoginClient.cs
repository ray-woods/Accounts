using Accounts.ApiInfrastructure.Responses.User;
using Accounts.Models.User;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.User
{
    public interface ILoginClient
    {
        Task<RegisterResponse> Register(RegisterViewModel viewModel);
        Task<TokenResponse> Login(string email, string password);
    }
}
