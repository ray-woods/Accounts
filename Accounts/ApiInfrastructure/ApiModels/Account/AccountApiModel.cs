using ApiHelper.Model;

namespace Accounts.ApiInfrastructure.ApiModels.Account
{
    public class AccountApiModel : ApiModel
    {
        public int AccountID { get; set; }
        public int AccountTypeID { get; set; }
        public string Name { get; set; }
    }
}