using Accounts.ApiInfrastructure.ApiModels.Transaction;
using Accounts.ApiInfrastructure.Responses.Transaction;
using Accounts.Core;
using Accounts.Models.Transaction;
using ApiHelper.Client;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Accounts.ApiInfrastructure.Client.Transaction
{
    public class DepositClient : ClientBase, IDepositClient
    {
        #region ctor

        public DepositClient(IApiClient apiClient) : base(apiClient)
        {
        }

        #endregion

        #region post

        public async Task<DepositResponse> Deposit(DepositViewModel deposit)
        {
            var apiModel = Mapper.Map<DepositApiModel>(deposit);
            //var apiModel = new DepositApiModel
            //{
            //    AccountID = deposit.AccountID
            //};
            var depositResponse = await PostEncodedContentWithResponse<DepositResponse, DepositApiModel, DepositApiModel>(Constants.DepositUri, apiModel);
            return depositResponse;
        }

        #endregion
    }
}