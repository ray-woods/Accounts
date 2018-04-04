using Accounts.ApiInfrastructure.ApiModels.Account;
using Accounts.ApiInfrastructure.ApiModels.AccountType;
using Accounts.ApiInfrastructure.Client.Account;
using Accounts.ApiInfrastructure.Client.AccountType;
using Accounts.Attributes;
using Accounts.Models.Account;
using ApiHelper.Client;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    [Authentication]
    public class AccountController : BaseController
    {
        #region ctor

        private readonly IAccountClient _accountClient;
        private readonly IAccountTypeClient _accountTypeClient;
        private readonly IApiClient _apiClient;
        private readonly HttpClient _httpClient;

        public AccountController(IAccountClient accountClient, IAccountTypeClient accountTypeClient, IApiClient apiClient, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _accountClient = accountClient;
            _accountTypeClient = accountTypeClient;
            _apiClient = apiClient;
        }

        #endregion

        #region Index

        // http://localhost:53001/Account/Index
        // GET: Account/Index
        public async Task<ActionResult> Index()
        {
            var accounts = await _accountClient.GetAccount();

            var viewModel = new AccountsViewModel();
            viewModel.Accounts = Mapper.Map<List<AccountApiModel>, List<AccountViewModel>>(accounts.Data);
            viewModel.AccountTypes = await GetAccountTypes();

            return View(viewModel);
        }

        #endregion

        #region Details

        // GET: Account/Account/1
        public async Task<ActionResult> Details(int id)
        {
            return await GetAccount(id);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = new AccountViewModel();
            model.AccountTypes = await GetAccountTypes();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountViewModel model)
        {
            var response = await _accountClient.CreateAccount(model);

            if (response.StatusIsSuccessful)
            {
                var accountId = response.Data;
                return RedirectToAction("Details", new { id = accountId });
            }

            AddResponseErrorsToModelState(response);
            model.AccountTypes = await GetAccountTypes();
            return View(model);
        }

        #endregion

        #region Edit

        public async Task<ActionResult> Edit(int id)
        {
            return await GetAccount(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccountViewModel model)
        {
            var response = await _accountClient.UpdateAccount(model);

            if (response.StatusIsSuccessful)
            {
                var accountId = response.Data;
                return RedirectToAction("Index");
            }

            AddResponseErrorsToModelState(response);
            model.AccountTypes = await GetAccountTypes();
            return View(model);
        }

        #endregion

        #region Delete

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _accountClient.DeleteAccount(id);

            if (response.StatusIsSuccessful)
            {
                var accountId = response.Data;
                return RedirectToAction("Index");
            }

            AddResponseErrorsToModelState(response);
            return RedirectToAction("Index"); 
        }

        #endregion

        #region Private methods

        private async Task<List<SelectListItem>> GetAccountTypes()
        {
            var accountTypes = await _accountTypeClient.GetAccountType();
            return Mapper.Map<List<AccountTypeApiModel>, List<SelectListItem>>(accountTypes.Data);
        }

        private async Task<ActionResult> GetAccount(int id)
        {
            var response = await _accountClient.GetAccount(id);
            var model = new AccountViewModel();

            if (response.StatusIsSuccessful)
            {
                model = Mapper.Map<AccountViewModel>(response.Data);
                model.AccountTypes = await GetAccountTypes();
                return View(model);
            }

            AddResponseErrorsToModelState(response);
            model.AccountTypes = await GetAccountTypes();
            return View(model);
        }

        #endregion
    }
}