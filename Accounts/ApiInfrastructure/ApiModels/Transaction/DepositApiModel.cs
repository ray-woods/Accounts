using ApiHelper.Model;
using System;

namespace Accounts.ApiInfrastructure.ApiModels.Transaction
{
    public class DepositApiModel : ApiModel
    {
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
        public int AssetTypeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? AccountingPeriodID { get; set; }

        public int CashBookTransactionID { get; set; }
        public int AccountTransactionID { get; set; }
        public int JournalID { get; set; }
    }
}