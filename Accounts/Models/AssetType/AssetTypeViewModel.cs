using System.ComponentModel.DataAnnotations;

namespace Accounts.Models.AssetType
{
    public class AssetTypeViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Asset Type ID")]
        public int AssetTypeID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Asset Type")]
        public string Name { get; set; }
    }
}