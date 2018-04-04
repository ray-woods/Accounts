using Accounts.ApiInfrastructure.ApiModels.AssetType;
using Accounts.ApiInfrastructure.Responses.AssetType;
using Accounts.Core;
using Accounts.Models.AssetType;
using ApiHelper.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.AssetType
{
    public class AssetTypeClient : ClientBase, IAssetTypeClient
    {
        #region ctor

        public AssetTypeClient(IApiClient apiClient) : base(apiClient)
        {
        }

        #endregion

        #region get

        public async Task<AssetTypesResponse> GetAssetType()
        {
            var response = await GetJsonDecodedContent<AssetTypesResponse, List<AssetTypeApiModel>>(Constants.AssetTypeUri, null);
            return response;
        }

        public async Task<AssetTypeResponse> GetAssetType(int assetTypeId)
        {
            var idPair = new KeyValuePair<string, string>("id", assetTypeId.ToString());
            var response = await GetJsonDecodedContent<AssetTypeResponse, AssetTypeApiModel>(Constants.AssetTypeUri, idPair);
            return response;
        }

        #endregion

        #region post

        public async Task<CreateAssetTypeResponse> CreateAssetType(AssetTypeViewModel assetType)
        {
            var apiModel = new AssetTypeApiModel
            {
                AssetTypeID = 0,
                Name = assetType.Name
            };
            var createProductResponse = await PostEncodedContentWithSimpleResponse<CreateAssetTypeResponse, AssetTypeApiModel>(Constants.AssetTypeUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region put

        public async Task<UpdateAssetTypeResponse> UpdateAssetType(AssetTypeViewModel assetType)
        {
            var apiModel = new AssetTypeApiModel
            {
                AssetTypeID = assetType.AssetTypeID,
                Name = assetType.Name
            };
            var createProductResponse = await PutEncodedContentWithSimpleResponse<UpdateAssetTypeResponse, AssetTypeApiModel>(Constants.AssetTypeUri, apiModel);
            return createProductResponse;
        }

        #endregion

        #region Delete

        public async Task<DeleteAssetTypeResponse> DeleteAssetType(int assetTypeId)
        {
            var apiModel = new AssetTypeApiModel
            {
                AssetTypeID = assetTypeId,
                Name = string.Empty
            };
            var deleteProductResponse = await DeleteEncodedContentWithSimpleResponse<DeleteAssetTypeResponse, AssetTypeApiModel>(Constants.AssetTypeUri, apiModel);
            return deleteProductResponse;
        }

        #endregion
    }
}