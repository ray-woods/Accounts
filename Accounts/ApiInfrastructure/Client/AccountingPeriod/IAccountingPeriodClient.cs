using Accounts.ApiInfrastructure.Responses.AccountingPeriod;
using Accounts.Models.AccountingPeriod;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AccountingPeriod
{
    public interface IAccountingPeriodClient
    {
        Task<AccountingPeriodsResponse> GetAccountingPeriod();
        Task<AccountingPeriodResponse> GetAccountingPeriod(int accountingPeriodId);
        Task<CreateAccountingPeriodResponse> CreateAccountingPeriod(AccountingPeriodViewModel accountingPeriod);
        Task<UpdateAccountingPeriodResponse> UpdateAccountingPeriod(AccountingPeriodViewModel accountingPeriod);
        Task<DeleteAccountingPeriodResponse> DeleteAccountingPeriod(int accountingPeriodId);
    }
}
