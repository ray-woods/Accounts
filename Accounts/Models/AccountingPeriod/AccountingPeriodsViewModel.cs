using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounts.Models.AccountingPeriod
{
    public class AccountingPeriodsViewModel
    {
        public AccountingPeriodsViewModel()
        {
            AccountingPeriods = new List<AccountingPeriodViewModel>();
        }

        [Display(Name = "Accounting Periods")]
        public IList<AccountingPeriodViewModel> AccountingPeriods { get; set; }
    }
}