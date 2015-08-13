using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class _Login : System.Web.UI.Page
{
    private static List<string> ListUsers;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ImportUser();
        }
    }

    private void ImportUser()
    {
        ListUsers = new List<string>();
        ListUsers.Add("12bet01");
        ListUsers.Add("spon02");
		ListUsers.Add("spon03");
		ListUsers.Add("spon07");
		ListUsers.Add("spon08");
		ListUsers.Add("spon09");
		ListUsers.Add("spon11");
		ListUsers.Add("spon12");
        ListUsers.Add("KiwiTest");
    }

    private bool ValidateUser(string userName, string password)
    {
        if (ReferenceEquals(null, ListUsers))
            ImportUser();

        return (ListUsers.Contains(userName) && password == "2conthanhlanhcon");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidateUser(txtUserName.Text.Trim().ToLower(), txtPassword.Text))
            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text.Trim().ToLower(), true);             

        lblMessage.Text = "Login fail!";
    }
}
