using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Nexcel.LRF.Integration;
using Nexcel.LRF.Integration.Configurations;

namespace SSOSimulator.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const int SpondemoLicenseeId = 5;

        public ActionResult Index()
        {
            return View();
        }

        public void GotoLRF()
        {
            SendRequestSso();
        }


        private void SendRequestSso()
        {
            if (!Request.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();
            var activeDomain = IntegrationSettings.Settings.SsoUrl;
            if(string.IsNullOrEmpty(activeDomain))
            {
                activeDomain = GetActiveDomain();
            }

            //create request sso model
            RequestSsoModel requestModel = new RequestSsoModel();
            requestModel.Username = HttpContext.User.Identity.Name;

            requestModel.EncryptedPassword = ConfigurationManager.AppSettings["UserPassword"];
            requestModel.Timestamp = GetDbTimeStamp();
            requestModel.ActiveDomain = activeDomain;
            requestModel.LanguageCode = "en-US";
            //generate sso link
            UrlFactory urlFactory = new UrlFactory();
            string ssoUrl = urlFactory.GenerateSsoLink(requestModel);
            Response.Redirect(ssoUrl);
        }

        /// <summary>
        /// get current timestamp from database
        /// have to be changed in internal site
        /// </summary>
        /// <returns></returns>
        private long GetDbTimeStamp()
        {
            using (SqlConnection sqlConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Lrf_GetDBCurrentDatetime", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    DateTime result = (DateTime)sqlCommand.ExecuteScalar();
                    sqlConnection.Close();
                    return result.Ticks;
                }
            }
        }

        private string GetActiveDomain()
        {
            using (SqlConnection sqlConnection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Lrf_GetActiveDomain", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    string result = (string)sqlCommand.ExecuteScalar();
                    sqlConnection.Close();
                    return result;
                }
            }
        }

        private string GetEncryptedPassword(string userName)
        {
            
            using (SqlConnection sqlConnection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Lrf_UserInfoSelByUserName", sqlConnection))
                {
                    var result = string.Empty;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@UserName", userName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LicenseeId", SpondemoLicenseeId));
                    sqlConnection.Open();
                    var reader = sqlCommand.ExecuteReader();
                    if(reader.Read())
                    {
                        result = reader["Password"].ToString();
                    }

                    sqlConnection.Close();
                    return result;
                }
            }
        }
    }
}