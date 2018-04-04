using Accounts.ApiInfrastructure.ApiModels.Account;
using Accounts.ApiInfrastructure.Responses.Account;
using Accounts.Core;
using Accounts.Models.Account;
using ApiHelper.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.Account
{
    public class AccountClient : ClientBase, IAccountClient
    {
        #region ctor

        public AccountClient(IApiClient apiClient) : base(apiClient)
        {
        }

        #endregion

        #region get

        public async Task<AccountsResponse> GetAccount()
        {
            var response = await GetJsonDecodedContent<AccountsResponse, List<AccountApiModel>>(Constants.AccountUri, null);
            return response;
        }

        public async Task<AccountResponse> GetAccount(int accountId)
        {
            var idPair = new KeyValuePair<string, string>("id", accountId.ToString());
            var response = await GetJsonDecodedContent<AccountResponse, AccountApiModel>(Constants.AccountUri, idPair);
            return response;
        }

        #endregion

        #region post

        public async Task<CreateAccountResponse> CreateAccount(AccountViewModel account)
        {
            var apiModel = new AccountApiModel
            {
                AccountID = 0,
                AccountTypeID = account.AccountTypeID,
                Name = account.Name
            };
            var createProductResponse = await PostEncodedContentWithSimpleResponse<CreateAccountResponse, AccountApiModel>(Constants.AccountUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region put

        public async Task<UpdateAccountResponse> UpdateAccount(AccountViewModel account)
        {
            var apiModel = new AccountApiModel
            {
                AccountID = account.AccountID,
                AccountTypeID = account.AccountTypeID,
                Name = account.Name
            };
            var createProductResponse = await PutEncodedContentWithSimpleResponse<UpdateAccountResponse, AccountApiModel>(Constants.AccountUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region Delete

        public async Task<DeleteAccountResponse> DeleteAccount(int accountId)
        {
            var apiModel = new AccountApiModel
            {
                AccountID = accountId,
                Name = string.Empty
            };
            var deleteProductResponse = await DeleteEncodedContentWithSimpleResponse<DeleteAccountResponse, AccountApiModel>(Constants.AccountUri, apiModel);
            return deleteProductResponse;
        }

        #endregion
    }
}