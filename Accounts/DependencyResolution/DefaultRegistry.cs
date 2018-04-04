// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Accounts.DependencyResolution
{
    using Accounts.ApiInfrastructure.Client.Account;
    using Accounts.ApiInfrastructure.Client.AccountingPeriod;
    using Accounts.ApiInfrastructure.Client.AccountType;
    using Accounts.ApiInfrastructure.Client.AssetType;
    using Accounts.ApiInfrastructure.Client.Transaction;
    using Accounts.Core;
    using ApiHelper.Client;
    using StructureMap;
    using System;
    using System.Net.Http;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IAccountClient>().Use<AccountClient>();
            For<IAccountTypeClient>().Use<AccountTypeClient>();
            For<IAccountingPeriodClient>().Use<AccountingPeriodClient>();
            For<IAssetTypeClient>().Use<AssetTypeClient>();
            For<HttpClient>().Use(() => new HttpClient() { BaseAddress = new Uri(Constants.ApiBaseUri) });
            For<IApiClient>().Use<ApiClient>();
            For<IDepositClient>().Use<DepositClient>();
        }

        #endregion
    }
}