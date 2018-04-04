using Accounts.ApiInfrastructure.Responses.AssetType;
using Accounts.Models.AssetType;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AssetType
{
    public interface IAssetTypeClient
    {
        Task<AssetTypesResponse> GetAssetType();
        Task<AssetTypeResponse> GetAssetType(int assetTypeId);
        Task<CreateAssetTypeResponse> CreateAssetType(AssetTypeViewModel assetType);
        Task<UpdateAssetTypeResponse> UpdateAssetType(AssetTypeViewModel assetType);
        Task<DeleteAssetTypeResponse> DeleteAssetType(int assetTypeId);
    }
}
