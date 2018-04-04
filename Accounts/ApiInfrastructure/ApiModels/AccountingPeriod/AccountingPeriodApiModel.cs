using ApiHelper.Model;
using System;

namespace Accounts.ApiInfrastructure.ApiModels.AccountingPeriod
{
    public class AccountingPeriodApiModel : ApiModel
    {
        public int AccountingPeriodID { get; set; }
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}