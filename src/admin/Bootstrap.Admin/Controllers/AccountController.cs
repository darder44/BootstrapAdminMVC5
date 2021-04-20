using Bootstrap.Admin.Models;
using Bootstrap.DataAccess;
using Bootstrap.Security.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bootstrap.Admin.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    [AllowAnonymous]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private const string MobileSchema = "Mobile";
        /// <summary>
        /// 系統鎖屏界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Lock()
        {
            if (!User.Identity!.IsAuthenticated) return Login();

            var authenticationType = User.Identity.AuthenticationType;
            await HttpContext.SignOutAsync();
            var urlReferrer = Request.Headers["Referer"].FirstOrDefault();
            if (urlReferrer?.Contains("/Pages", StringComparison.OrdinalIgnoreCase) ?? false) urlReferrer = "/Pages";
            return View(new LockModel(User.Identity.Name)
            {
                AuthenticationType = authenticationType,
                ReturnUrl = WebUtility.UrlEncode(string.IsNullOrEmpty(urlReferrer) ? CookieAuthenticationDefaults.LoginPath.Value : urlReferrer)
            });
        }

        /// <summary>
        /// 系統登入方法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login([FromQuery] string? appId = null, [FromQuery] string view = "")
        {
            if (DictHelper.RetrieveSystemModel())
            {
                ViewBag.UserName = "Admin";
                ViewBag.Password = "123789";
            }
            return User.Identity!.IsAuthenticated ? (ActionResult)Redirect("~/Admin/Index") : LoginView(view, new LoginModel(appId));
        }

        private ViewResult LoginView(string view, LoginModel model)
        {
            if (string.IsNullOrEmpty(view))
            {
                // retrieve login view from db
                view = DictHelper.RetrieveLoginView();
            }
            return View(view, model);
        }


        private IActionResult RedirectLogin()
        {
            var query = Request.Query.Aggregate(new Dictionary<string, string?>(), (d, v) =>
            {
                d.Add(v.Key, v.Value.ToString());
                return d;
            });
            return Redirect(QueryHelpers.AddQueryString(Request.PathBase + CookieAuthenticationDefaults.LoginPath, query));
        }

        /// <summary>
        /// Login the specified userName, password and remember.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="remember">Remember.</param>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string remember)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return RedirectLogin();

            var auth = UserHelper.Authenticate(userName, password);
            HttpContext.Log(userName, auth);
            return auth ? await SignInAsync(userName, remember == "true") : LoginView("", new LoginModel() { AuthFailed = true });
        }

        private async Task<IActionResult> SignInAsync(string userName, bool persistent, string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var identity = new ClaimsIdentity(authenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { ExpiresUtc = DateTimeOffset.Now.AddDays(DictHelper.RetrieveCookieExpiresPeriod()), IsPersistent = persistent });

            // redirect origin url
            var originUrl = Request.Query[CookieAuthenticationDefaults.ReturnUrlParameter].FirstOrDefault() ?? "~/Home/Index";
            return Redirect(originUrl);
        }

        /// <summary>
        /// Logout this instance.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns>The logout.</returns>
        [HttpGet]
        public async Task<IActionResult> Logout([FromQuery] string appId)
        {
            await HttpContext.SignOutAsync();
            return Redirect(QueryHelpers.AddQueryString(Request.PathBase + CookieAuthenticationDefaults.LoginPath, "AppId", appId ?? BootstrapAppContext.AppId));
        }

        /// <summary>
        /// Accesses the denied.
        /// </summary>
        /// <returns>The denied.</returns>
        [ResponseCache(Duration = 600)]
        [HttpGet]
        public ActionResult AccessDenied() => View("Error", ErrorModel.CreateById(403));

    }
}
