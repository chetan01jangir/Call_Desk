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

public partial class User_ContactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//        if (Session["BranchName"].ToString() == "Agent Branch")
//        {
//            trIMD.Visible = true;
//            trSupport.Visible = false;
//        }
//        else
//        {
//            trIMD.Visible = false;
//            trSupport.Visible = true;
//
//        }
			  trIMD.Visible = true;

    }
}
