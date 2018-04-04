using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounts.Models.AssetType
{
    public class AssetTypesViewModel
    {
        public AssetTypesViewModel()
        {
            AssetTypes = new List<AssetTypeViewModel>();
        }

        [Display(Name = "Asset Types")]
        public IList<AssetTypeViewModel> AssetTypes { get; set; }
    }
}