using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Accounts.Models.Account
{
    public class AccountsViewModel
    {
        public AccountsViewModel()
        {
            Accounts = new List<AccountViewModel>();
            AccountTypes = new List<SelectListItem>();
        }

        [Display(Name = "Accounts")]
        public IList<AccountViewModel> Accounts { get; set; }

        [Display(Name = "Account Type")]
        public List<SelectListItem> AccountTypes { get; set; }
    }
}