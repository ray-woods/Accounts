using Accounts.ApiInfrastructure.ApiModels.User;
using Accounts.ApiInfrastructure.Responses.User;
using Accounts.Core;
using Accounts.Models.User;
using ApiHelper.Client;
using ApiHelper.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.ApiInfrastructure.Client.User
{
    public class LoginClient : ClientBase, ILoginClient
    {

        #region ctor

        private readonly IApiClient _apiClient;

        public LoginClient(IApiClient apiClient) : base(apiClient)
        {
            _apiClient = apiClient;
        }

        #endregion

        #region Register

        public async Task<RegisterResponse> Register(RegisterViewModel viewModel)
        {
            var apiModel = new RegisterApiModel
            {
                ConfirmPassword = viewModel.ConfirmPassword,
                Email = viewModel.Email,
                Password = viewModel.Password
            };
            var response = await _apiClient.PostJsonEncodedContent(Constants.RegisterUri, apiModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);
            return registerResponse;
        }

        #endregion

        #region Login

        public async Task<TokenResponse> Login(string email, string password)
        {
            var response = await apiClient.PostFormEncodedContent(Constants.TokenUri, "grant_type".AsPair("password"),
                "username".AsPair(email), "password".AsPair(password));
            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                    {
                        {errorContent["error"], new string[] {errorContent["error_description"]}}
                    }
                };

                return tokenResponse;
            }

            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            return tokenResponse;
        }
        
        #endregion

    }
}