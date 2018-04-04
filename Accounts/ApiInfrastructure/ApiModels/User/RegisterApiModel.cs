using ApiHelper.Model;

namespace Accounts.ApiInfrastructure.ApiModels.User
{
    public class RegisterApiModel : ApiModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}