using Accounts.ApiInfrastructure.ApiModels.AccountType;
using Accounts.ApiInfrastructure.Responses.AccountType;
using Accounts.Core;
using ApiHelper.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AccountType
{
    public class AccountTypeClient : ClientBase, IAccountTypeClient
    {
        #region ctor

        public AccountTypeClient(IApiClient apiClient) : base(apiClient)
        {
        }

        #endregion

        #region get

        public async Task<AccountTypesResponse> GetAccountType()
        {
            var response = await GetJsonDecodedContent<AccountTypesResponse, List<AccountTypeApiModel>>(Constants.AccountTypeUri, null);
            return response;
        }

        public async Task<AccountTypeResponse> GetAccountType(int accountTypeId)
        {
            var idPair = new KeyValuePair<string, string>("id", accountTypeId.ToString());
            var response = await GetJsonDecodedContent<AccountTypeResponse, AccountTypeApiModel>(Constants.AccountTypeUri, idPair);
            return response;
        }

        #endregion
    }
}