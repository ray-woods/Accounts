using System.ComponentModel.DataAnnotations;

namespace Accounts.Models.AccountType
{
    public class AccountTypeViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account Type ID")]
        public int AccountTypeID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account Type")]
        public string Name { get; set; }
    }
}