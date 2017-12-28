using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Nexcel.LRF.Integration;
using System.Data.SqlClient;


public partial class _RedirectSso : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SendRequestSso();
    }

    protected void SendRequestSso()
    {
        if (!Request.IsAuthenticated)
            FormsAuthentication.RedirectToLoginPage();

        //create request sso model
        RequestSsoModel requestModel = new RequestSsoModel();
        requestModel.Username = HttpContext.Current.User.Identity.Name;

        requestModel.EncryptedPassword = "4A76D27A7541FB91F9563806F6C1E27485A9BDAD";
        requestModel.Timestamp = GetDbTimeStamp();
        requestModel.ActiveDomain = GetActiveDomain();
        requestModel.LanguageCode = "zh-CN";
        requestModel.LicenseeName = "12bet";
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
    protected long GetDbTimeStamp()
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
    protected string GetActiveDomain()
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
}