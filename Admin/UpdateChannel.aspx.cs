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
using CallDeskBO;
using CallDeskBAL;

public partial class Admin_UpdateChannel : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfChannelID.Value = Convert.ToString(Session["ChannelID"]);
            hfChannel.Value = Convert.ToString(Session["Channel"]);
            txtChannel.Text = hfChannel.Value;
        }
    }

    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ChannelBO objBO = new ChannelBO();
            ChannelBAL objBAL = new ChannelBAL();
            objBO.ChannelID = Convert.ToInt32(hfChannelID.Value);
            objBO.Channel = txtChannel.Text.Trim();
            objBO.UpdatedBy = Membership.GetUser().UserName;
            int intReturnValue = objBAL.UpdateChannel(objBO.ChannelID, objBO.Channel, objBO.UpdatedBy);
            Response.Redirect("AddChannel.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Code Button Cancel

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddChannel.aspx");
    }

    #endregion
    
}
