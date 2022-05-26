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
using CallDeskBAL;

public partial class User_EscalationDetails : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
    }
    #endregion       

  
}
