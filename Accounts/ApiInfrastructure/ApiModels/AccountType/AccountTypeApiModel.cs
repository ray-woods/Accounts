using ApiHelper.Model;

namespace Accounts.ApiInfrastructure.ApiModels.AccountType
{
    public class AccountTypeApiModel : ApiModel
    {
        public int AccountTypeID { get; set; }
        public string Name { get; set; }
    }
}