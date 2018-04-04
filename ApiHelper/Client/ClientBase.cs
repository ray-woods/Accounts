using ApiHelper.Model;
using ApiHelper.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace ApiHelper.Client
{
    public abstract class ClientBase
    {
        #region ctor

        protected readonly IApiClient apiClient;

        protected ClientBase(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        #endregion

        #region Get

        protected async Task<TResponse> GetJsonDecodedContent<TResponse, TContentResponse>(string uri, params KeyValuePair<string, string>[] requestParameters) 
            where TResponse : ApiResponse<TContentResponse>, new()
        {
            var apiResponse = await apiClient.GetFormEncodedContent(uri, requestParameters);
            var response = await CreateJsonResponse<TResponse>(apiResponse);
            response.Data = Json.Decode<TContentResponse>(response.ResponseResult);
            return response;
        }

        #endregion

        #region Post

        protected async Task<TResponse> PostEncodedContentWithSimpleResponse<TResponse, TModel>(string url, TModel model)
        where TModel : ApiModel
        where TResponse : ApiResponse<int>, new()
            {
                using (var apiResponse = await apiClient.PostJsonEncodedContent(url, model))
                {
                    var response = await CreateJsonResponse<TResponse>(apiResponse);
                    response.Data = Json.Decode<int>(response.ResponseResult);
                    return response;
                }
            }

        protected async Task<TResponse> PostEncodedContentWithResponse<TResponse, TModel, TContentResponse>(string url, TModel model)
            where TModel : ApiModel
            where TResponse : ApiResponse<TContentResponse>, new()
        {
            using (var apiResponse = await apiClient.PostJsonEncodedContent(url, model))
            {
                var response = await CreateJsonResponse<TResponse>(apiResponse);
                response.Data = Json.Decode<TContentResponse>(response.ResponseResult);
                return response;
            }
        }

        #endregion

        #region Put

        protected async Task<TResponse> PutEncodedContentWithSimpleResponse<TResponse, TModel>(string url, TModel model)
        where TModel : ApiModel
        where TResponse : ApiResponse<bool>, new()
        {
            using (var apiResponse = await apiClient.PutJsonEncodedContent(url, model))
            {
                var response = await CreateJsonResponse<TResponse>(apiResponse);
                response.Data = Json.Decode<bool>(response.ResponseResult);
                return response;
            }
        }

        #endregion

        #region Delete

        protected async Task<TResponse> DeleteEncodedContentWithSimpleResponse<TResponse, TModel>(string url, TModel model)
        where TModel : ApiModel
        where TResponse : ApiResponse<bool>, new()
        {
            using (var apiResponse = await apiClient.DeleteJsonEncodedContent(url, model))
            {
                var response = await CreateJsonResponse<TResponse>(apiResponse);
                response.Data = Json.Decode<bool>(response.ResponseResult);
                return response;
            }
        }

        #endregion

        #region Helpers

        public static async Task<TContentResponse> DecodeContent<TContentResponse>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return Json.Decode<TContentResponse>(result);
        }

        #endregion

        #region Private methods

        protected static async Task<TResponse> CreateJsonResponse<TResponse>(HttpResponseMessage response) where TResponse : ApiResponse, new()
        {
            var clientResponse = new TResponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ErrorState = response.IsSuccessStatusCode ? null : await DecodeContent<ErrorStateResponse>(response),
                ResponseCode = response.StatusCode
            };

            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }

        #endregion

    }
}
