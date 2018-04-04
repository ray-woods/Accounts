using Accounts.ApiInfrastructure.ApiModels.AccountingPeriod;
using Accounts.ApiInfrastructure.Client.AccountingPeriod;
using Accounts.Attributes;
using Accounts.Models.AccountingPeriod;
using ApiHelper.Client;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    [Authentication]
    public class AccountingPeriodController : BaseController
    {
        #region ctor

        private readonly IAccountingPeriodClient _accountingPeriodClient;
        private readonly IApiClient _apiClient;
        private readonly HttpClient _httpClient;

        public AccountingPeriodController(IAccountingPeriodClient accountingPeriodClient, IApiClient apiClient, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _accountingPeriodClient = accountingPeriodClient;
            _apiClient = apiClient;
        }

        #endregion

        #region Index

        // GET: AccountingPeriod/Index
        public async Task<ActionResult> Index()
        {
            var accountingPeriods = await _accountingPeriodClient.GetAccountingPeriod();

            var viewModel = new AccountingPeriodsViewModel();
            viewModel.AccountingPeriods = Mapper.Map<List<AccountingPeriodApiModel>, List<AccountingPeriodViewModel>>(accountingPeriods.Data);

            return View(viewModel);
        }

        #endregion

        #region Details

        public async Task<ActionResult> Details(int id)
        {
            return await GetAccountingPeriod(id);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            var model = new AccountingPeriodViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountingPeriodViewModel model)
        {
            var response = await _accountingPeriodClient.CreateAccountingPeriod(model);

            if (response.StatusIsSuccessful)
            {
                var accountingPeriodID = response.Data;
                return RedirectToAction("Details", new { id = accountingPeriodID });
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion

        #region Edit

        public async Task<ActionResult> Edit(int id)
        {
            return await GetAccountingPeriod(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccountingPeriodViewModel model)
        {
            var response = await _accountingPeriodClient.UpdateAccountingPeriod(model);

            if (response.StatusIsSuccessful)
            {
                var accountId = response.Data;
                return RedirectToAction("Index");
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion

        #region Delete

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _accountingPeriodClient.DeleteAccountingPeriod(id);

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

        private async Task<ActionResult> GetAccountingPeriod(int id)
        {
            var response = await _accountingPeriodClient.GetAccountingPeriod(id);
            var model = new AccountingPeriodViewModel();

            if (response.StatusIsSuccessful)
            {
                model = Mapper.Map<AccountingPeriodViewModel>(response.Data);
                return View(model);
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion
    }
}