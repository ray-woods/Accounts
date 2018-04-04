using ApiHelper.Model;

namespace Accounts.ApiInfrastructure.ApiModels.AssetType
{
    public class AssetTypeApiModel : ApiModel
    {
        public int AssetTypeID { get; set; }
        public string Name { get; set; }
    }
}