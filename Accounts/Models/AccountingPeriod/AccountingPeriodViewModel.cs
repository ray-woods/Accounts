using System;
using System.ComponentModel.DataAnnotations;

namespace Accounts.Models.AccountingPeriod
{
    public class AccountingPeriodViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Accounting Period ID")]
        public int AccountingPeriodID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Accounting Period Name")]
        public string Name { get; set; }

        [Display(Name = "Valid From")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ValidFrom
        {
            get
            {
                return validFrom ?? DateTime.MinValue;
            }
            set
            {
                validFrom = value;
            }
        }
        private DateTime? validFrom;

        [Display(Name = "Valid To")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ValidTo
        {
            get
            {
                return validTo ?? DateTime.MaxValue;
            }
            set
            {
                validTo = value;
            }
        }
        private DateTime? validTo;
    }
}