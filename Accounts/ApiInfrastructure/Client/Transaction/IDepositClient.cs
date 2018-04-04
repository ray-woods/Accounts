using Accounts.ApiInfrastructure.Responses.Transaction;
using Accounts.Models.Transaction;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.Transaction
{
    public interface IDepositClient
    {
        Task<DepositResponse> Deposit(DepositViewModel deposit);
    }
}
