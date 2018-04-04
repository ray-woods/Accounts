using Accounts.ApiInfrastructure.Responses.Account;
using Accounts.Models.Account;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.Account
{
    public interface IAccountClient
    {
        Task<AccountsResponse> GetAccount();
        Task<AccountResponse> GetAccount(int accountId);
        Task<CreateAccountResponse> CreateAccount(AccountViewModel account);
        Task<UpdateAccountResponse> UpdateAccount(AccountViewModel account);
        Task<DeleteAccountResponse> DeleteAccount(int accountId);
    }
}
