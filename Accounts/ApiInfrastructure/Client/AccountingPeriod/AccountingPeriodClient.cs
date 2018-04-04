using Accounts.ApiInfrastructure.ApiModels.AccountingPeriod;
using Accounts.ApiInfrastructure.Responses.AccountingPeriod;
using Accounts.Core;
using Accounts.Models.AccountingPeriod;
using ApiHelper.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AccountingPeriod
{
    public class AccountingPeriodClient : ClientBase, IAccountingPeriodClient
    {
        #region ctor

        public AccountingPeriodClient(IApiClient apiClient) : base(apiClient)
        {
        }

        #endregion

        #region get

        public async Task<AccountingPeriodsResponse> GetAccountingPeriod()
        {
            var response = await GetJsonDecodedContent<AccountingPeriodsResponse, List<AccountingPeriodApiModel>>(Constants.AccountingPeriodUri, null);
            return response;
        }

        public async Task<AccountingPeriodResponse> GetAccountingPeriod(int accountingPeriodId)
        {
            var idPair = new KeyValuePair<string, string>("id", accountingPeriodId.ToString());
            var response = await GetJsonDecodedContent<AccountingPeriodResponse, AccountingPeriodApiModel>(Constants.AccountingPeriodUri, idPair);
            return response;
        }

        #endregion

        #region post

        public async Task<CreateAccountingPeriodResponse> CreateAccountingPeriod(AccountingPeriodViewModel accountingPeriod)
        {
            var apiModel = new AccountingPeriodApiModel
            {
                AccountingPeriodID = 0,
                Name = accountingPeriod.Name,
                ValidFrom = accountingPeriod.ValidFrom,
                ValidTo = accountingPeriod.ValidTo
            };
            var createProductResponse = await PostEncodedContentWithSimpleResponse<CreateAccountingPeriodResponse, AccountingPeriodApiModel>(Constants.AccountingPeriodUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region put

        public async Task<UpdateAccountingPeriodResponse> UpdateAccountingPeriod(AccountingPeriodViewModel accountingPeriod)
        {
            var apiModel = new AccountingPeriodApiModel
            {
                AccountingPeriodID = accountingPeriod.AccountingPeriodID,
                Name = accountingPeriod.Name,
                ValidFrom = accountingPeriod.ValidFrom,
                ValidTo = accountingPeriod.ValidTo
            };
            var createProductResponse = await PutEncodedContentWithSimpleResponse<UpdateAccountingPeriodResponse, AccountingPeriodApiModel>(Constants.AccountingPeriodUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region Delete

        public async Task<DeleteAccountingPeriodResponse> DeleteAccountingPeriod(int accountingPeriodId)
        {
            var apiModel = new AccountingPeriodApiModel
            {
                AccountingPeriodID = accountingPeriodId,
                Name = string.Empty
            };
            var deleteProductResponse = await DeleteEncodedContentWithSimpleResponse<DeleteAccountingPeriodResponse, AccountingPeriodApiModel>(Constants.AccountingPeriodUri, apiModel);
            return deleteProductResponse;
        }

        #endregion
    }
}