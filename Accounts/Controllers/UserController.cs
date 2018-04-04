using Accounts.ApiInfrastructure.Client.User;
using Accounts.Models.User;
using ApiHelper;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    public class UserController : BaseController
    {
        #region ctor

        private readonly ILoginClient _loginClient;
        private readonly ITokenContainer _tokenContainer;

        public UserController(ILoginClient loginClient, ITokenContainer tokenContainer)
        {
            this._loginClient = loginClient;
            this._tokenContainer = tokenContainer;
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var response = await _loginClient.Register(model);
            if (response.StatusIsSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var loginSuccess = await PerformLoginActions(model.Email, model.Password);
            if (loginSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.Clear();
            ModelState.AddModelError("", "The username or password is incorrect");
            return View(model);
        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            _tokenContainer.ApiToken = null;
            return RedirectToAction("Login", "User");
        }

        #endregion

        #region Private methods

        private async Task<bool> PerformLoginActions(string email, string password)
        {
            var response = await _loginClient.Login(email, password);
            if (response.StatusIsSuccessful)
            {
                _tokenContainer.ApiToken = response.Data;
            }
            else
            {
                AddResponseErrorsToModelState(response);
            }

            return response.StatusIsSuccessful;
        }

        #endregion

    }
}