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

public partial class InvalidAccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = "You have opened this webpage in a new tab or a new window, this window will close by the system.";

        if ((Request.QueryString["msg"] != null))
        {
            msg = Request.QueryString["msg"];
        }
        div1.InnerHtml = msg + "<BR>" + div1.InnerText;
        ClientScript.RegisterStartupScript(typeof(ClientScriptManager), "SCRIPTNAME", "<script language='javascript' type='text/javascript'> alert('" + msg + "');window.close();</script>");
	
    }
}
