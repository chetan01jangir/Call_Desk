using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserControls_MenuUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkregister_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://calldesk.reliancegeneral.co.in/User/mRegisterCall.aspx");
    }
    protected void lnktrackcall_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://calldesk.reliancegeneral.co.in/User/mTrackCall.aspx");
    }
    protected void lnkcontact_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://calldesk.reliancegeneral.co.in/User/mContactus.aspx");
    }
}
