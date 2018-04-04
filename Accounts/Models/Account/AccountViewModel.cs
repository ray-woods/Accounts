using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Accounts.Models.Account
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            AccountTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account Type")]
        public int AccountTypeID { get; set; }
        public List<SelectListItem> AccountTypes { get; set; }
    }
}