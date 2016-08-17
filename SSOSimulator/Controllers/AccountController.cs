using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            if (Session["UserLogin"] != null)
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

            var result = GetUserLogin(model.UserName);

            if (model.Password.Equals(ConfigurationManager.AppSettings["LoginPassword"]) && result != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                Session["UserLogin"] = result;
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

        private static LoginResultViewModel GetUserLogin(string userName)
        {
            using (SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "Select UserPwd, LicenseeName from Users where Username = @Username", sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = userName;
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new LoginResultViewModel
                            {
                                UserName = userName,
                                EncryptedPassword = reader.GetString(0),
                                LicenseeName = reader.GetString(1)
                            };
                        }
                    }

                    return null;
                }
            }
        }
    }
}