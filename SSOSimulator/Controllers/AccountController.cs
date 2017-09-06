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

            var result = GetUserLogin(model.UserName, model.LicenseeId);

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

        private static LoginResultViewModel GetUserLogin(string userName, string licenseeId)
        {
            using (SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Lrf_UserInfoSelByUserName", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = userName;
                    sqlCommand.Parameters.Add("@LicenseeId", SqlDbType.Int).Value = licenseeId;
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new LoginResultViewModel
                            {
                                UserName = userName,
                                EncryptedPassword = reader.GetString(reader.GetOrdinal("password")),
                                LicenseeName = GetLicenseeNameById(licenseeId)
                            };
                        }
                    }

                    return null;
                }
            }
        }

        private static string GetLicenseeNameById(string licenseeId)
        {
            switch (licenseeId)
            {
                case "1": return "12bet";
                case "3": return "mansion88";
                case "5": return "spondemo";
                default: return "12bet";
            }
        }
    }
}