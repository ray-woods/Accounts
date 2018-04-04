using Accounts.Service.Interfaces.Services;
using AccountsAPI.Models.Account;
using System.Collections.Generic;
using System.Web.Http;

namespace AccountsAPI.Controllers
{
    public class AccountController : ApiController
    {
        #region ctor

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        // GET: api/Account
        public IEnumerable<AccountApiModel> Get()
        {
            var returnValue = new List<AccountApiModel>();
            var accounts = _accountService.GetAccounts();

            foreach (var account in accounts)
            {
                var newAccount = new AccountApiModel();
                newAccount.AccountID = account.AccountID;
                newAccount.Name = account.Name;
                returnValue.Add(newAccount);
            }

            return returnValue;
        }

        // GET: api/Account/1
        public AccountApiModel Get(int id)
        {
            var result = new AccountApiModel();

            var account = _accountService.GetAccount(id);

            result.AccountID = account.AccountID;
            result.Name = account.Name;

            return result;
        }

        // POST: api/Account
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
