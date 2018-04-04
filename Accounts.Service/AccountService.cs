using Accounts.Core.Interfaces.Repositories;
using Accounts.Service.Interfaces.Services;
using Accounts.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Service
{
    public class AccountService : ServiceBase, IAccountService
    {
        #region ctor

        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        #endregion

        public int? CreateAccount(string accountName)
        {
            var account = new Account();
            account.Name = accountName;
            var accountID = _uow.Repository<Account>().InsertUpdate(account, account.AccountID);
            return accountID == 0 ? (int?)null : accountID;
        }

        public Account GetAccount(int accountID)
        {
            var account = _uow.Repository<Account>().FirstOrDefault(f => f.AccountID == accountID);
            return account;
        }

        public IList<Account> GetAccounts()
        {
            return _uow.Repository<Account>().All().ToList();
        }
    }
}
