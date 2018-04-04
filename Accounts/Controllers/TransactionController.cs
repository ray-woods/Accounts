using Accounts.ApiInfrastructure.ApiModels.Account;
using Accounts.ApiInfrastructure.ApiModels.AccountingPeriod;
using Accounts.ApiInfrastructure.ApiModels.AssetType;
using Accounts.ApiInfrastructure.Client.Account;
using Accounts.ApiInfrastructure.Client.AccountingPeriod;
using Accounts.ApiInfrastructure.Client.AssetType;
using Accounts.ApiInfrastructure.Client.Transaction;
using Accounts.Attributes;
using Accounts.Core;
using Accounts.Models.Transaction;
using ApiHelper.Client;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    [Authentication]
    public class TransactionController : BaseController
    {
        #region ctor

        private readonly IAccountClient _accountClient;
        private readonly IAccountingPeriodClient _accountingPeriodClient;
        private readonly IAssetTypeClient _assetTypeClient;
        private readonly IApiClient _apiClient;
        private readonly HttpClient _httpClient;
        private readonly IDepositClient _depositClient;

        public TransactionController(IAccountClient accountClient, 
            IAccountingPeriodClient accountingPeriodClient, 
            IAssetTypeClient assetTypeClient, 
            IApiClient apiClient, 
            HttpClient httpClient,
            IDepositClient depositClient
            )
        {
            _httpClient = httpClient;
            _accountClient = accountClient;
            _accountingPeriodClient = accountingPeriodClient;
            _assetTypeClient = assetTypeClient;
            _apiClient = apiClient;
            _depositClient = depositClient;
        }

        #endregion

        #region Deposit

        [HttpGet]
        public async Task<ActionResult> Deposit()
        {
            var model = new DepositViewModel();

            model.Accounts = await GetAccounts();
            model.AccountingPeriods = await GetAccountingPeriods();
            model.AssetTypes = await GetAssetTypes();
            model.TransactionDate = DateTime.Now.Date;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deposit(DepositViewModel model)
        {
            var response = await _depositClient.Deposit(model);

            if (response.StatusIsSuccessful)
            {
                var journalID = response.Data.JournalID;
                return RedirectToAction("Details", new { id = journalID });
            }

            AddResponseErrorsToModelState(response);

            model.Accounts = await GetAccounts();
            model.AccountingPeriods = await GetAccountingPeriods();
            model.AssetTypes = await GetAssetTypes();
            model.TransactionDate = DateTime.Now.Date;

            return View(model);
        }

        #endregion

        #region Private methods

        private async Task<List<SelectListItem>> GetAccounts()
        {
            var accounts = await _accountClient.GetAccount();
            return Mapper.Map<List<AccountApiModel>, List<SelectListItem>>(accounts.Data.Where(w => w.AccountID != Constants.CashBookAccountID).ToList());
        }

        private async Task<List<SelectListItem>> GetAccountingPeriods()
        {
            var accountingPeriods = await _accountingPeriodClient.GetAccountingPeriod();
            return Mapper.Map<List<AccountingPeriodApiModel>, List<SelectListItem>>(accountingPeriods.Data);
        }

        private async Task<List<SelectListItem>> GetAssetTypes()
        {
            var assetTypes = await _assetTypeClient.GetAssetType();
            return Mapper.Map<List<AssetTypeApiModel>, List<SelectListItem>>(assetTypes.Data);
        }

        #endregion
    }
}