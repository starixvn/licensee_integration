using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using SSOSimulator.Models;

namespace SSOSimulator.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Session["UserName"] != null)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ConfigurationManager.AppSettings["UserNames"].Contains(model.UserName) &&
                model.Password.Equals(ConfigurationManager.AppSettings["LoginPassword"]))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                Session["UserName"] = model.UserName;
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Wrong username or password.");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}