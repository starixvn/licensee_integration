using System;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Request.IsAuthenticated)
            FormsAuthentication.RedirectToLoginPage();
    }
}