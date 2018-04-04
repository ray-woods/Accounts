using Accounts.DAL;
using System.Collections.Generic;

namespace Accounts.Service.Interfaces.Services
{
    public interface IAccountService
    {
        int? CreateAccount(string accountName);
        Account GetAccount(int accountID);
        IList<Account> GetAccounts();
    }
}
