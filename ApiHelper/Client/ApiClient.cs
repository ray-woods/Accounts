using ApiHelper.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiHelper.Client
{
    public class ApiClient : IApiClient
    {
        #region ctor 

        private readonly HttpClient httpClient;
        private readonly ITokenContainer tokenContainer;

        public ApiClient(HttpClient httpClient, ITokenContainer tokenContainer)
        {
            this.httpClient = httpClient;
            this.tokenContainer = tokenContainer;
        }

        #endregion

        #region Get

        public async Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            AddToken();

            if (values != null)
            {
                using (var content = new FormUrlEncodedContent(values))
                {
                    var query = await content.ReadAsStringAsync();
                    var requestUriWithQuery = string.Concat(requestUri, "?", query);
                    var response = await httpClient.GetAsync(requestUriWithQuery);
                    return response;
                }
            }
            else
            {
                var response = await httpClient.GetAsync(requestUri);
                return response;
            }
        }

        #endregion

        #region Post

        public async Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AddToken();
            var response = await httpClient.PostAsJsonAsync(requestUri, content);
            return response;
        }

        public async Task<HttpResponseMessage> PostFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            using (var content = new FormUrlEncodedContent(values))
            {
                var response = await httpClient.PostAsync(requestUri, content);
                return response;
            }
        }

        #endregion

        #region Put

        public async Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AddToken();
            var response = await httpClient.PutAsJsonAsync(requestUri, content);
            return response;
        }

        #endregion

        #region Delete

        public async Task<HttpResponseMessage> DeleteJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AddToken();
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(content) });
            return response;
        }

        #endregion

        #region Private methods

        private static HttpContent Serialize(object data) => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        private void AddToken()
        {
            if (tokenContainer.ApiToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenContainer.ApiToken.ToString());
            }
        }

        #endregion
    }
}
