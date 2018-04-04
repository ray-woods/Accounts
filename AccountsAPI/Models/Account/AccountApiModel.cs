using System.ComponentModel.DataAnnotations;

namespace AccountsAPI.Models.Account
{
    public class AccountApiModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account Name")]
        public string Name { get; set; }
    }
}