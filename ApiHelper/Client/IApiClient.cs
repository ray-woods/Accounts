using ApiHelper.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper.Client
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values);
        Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel;
        Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel;
        Task<HttpResponseMessage> DeleteJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel;
        Task<HttpResponseMessage> PostFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values);
    }
}
