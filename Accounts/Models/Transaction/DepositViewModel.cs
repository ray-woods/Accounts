using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Accounts.Models.Transaction
{
    public class DepositViewModel
    {
        public DepositViewModel()
        {
            Accounts = new List<SelectListItem>();
            AccountingPeriods = new List<SelectListItem>();
            AssetTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Posting ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Transaction ID")]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Account")]
        public int AccountID { get; set; }
        public List<SelectListItem> Accounts { get; set; }

        //[Required(ErrorMessage = "Required")]
        //[Display(Name = "Journal ID")]
        //public int JournalID { get; set; }

        [Display(Name = "Accounting Period")]
        public int? AccountingPeriodID { get; set; }
        public List<SelectListItem> AccountingPeriods { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Asset Type")]
        public int AssetTypeID { get; set; }
        public List<SelectListItem> AssetTypes { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
    }
}