using ApiHelper.Response;
using System.Linq;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void AddResponseErrorsToModelState(ApiResponse response)
        {
            var errors = response.ErrorState.ModelState;
            if (errors == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                foreach (var entry in
                    from entry in ModelState
                    let matchSuffix = string.Concat(".", entry.Key)
                    where error.Key.EndsWith(matchSuffix)
                    select entry)
                {
                    ModelState.AddModelError(entry.Key, error.Value[0]);
                }

                if (error.Key == string.Empty)
                {
                    ModelState.AddModelError(string.Empty, error.Value[0]);
                }
            }
        }
    }
}