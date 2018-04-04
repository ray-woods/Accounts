using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsAPI.Models.Account
{
    public class AccountsApiModel
    {
        public AccountsApiModel()
        {
            Accounts = new List<AccountApiModel>();
        }

        [Display(Name = "Accounts")]
        public IList<AccountApiModel> Accounts { get; set; }
    }
}