using Accounts.ApiInfrastructure.Responses.AccountType;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AccountType
{
    public interface IAccountTypeClient
    {
        Task<AccountTypesResponse> GetAccountType();
        Task<AccountTypeResponse> GetAccountType(int accountTypeId);
    }
}
