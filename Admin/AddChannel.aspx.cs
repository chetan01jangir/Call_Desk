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

public partial class Admin_AddChannel : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetChannel();
        }
    }
    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ChannelBO objBO = new ChannelBO();
        ChannelBAL objBAL = new ChannelBAL();
        objBO.Channel = txtChannel.Text.Trim();

        if (objBAL.CheckExistingChannel(objBO.Channel) == "0")
        {
            lblMessage.Text = "Channel already exists.....";
        }
        else
        {
            objBO.CreatedBy = Membership.GetUser().UserName;
            objBO.Channel = txtChannel.Text.Trim();
            int i = objBAL.AddNewChannel(objBO.Channel, objBO.CreatedBy);
            GetChannel();
            lblMessage.Text = "Channel added successfully.....";
        }
    }
    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Method to Get the Channel

    public void GetChannel()
    {
        try
        {
            ChannelBAL objBAL = new ChannelBAL();
            gvChannel.DataSource = objBAL.GetChannel();
            gvChannel.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Channel Grid Row Command event

    protected void gvChannel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ChannelBO objBO = new ChannelBO();
            objBO.ChannelID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditChannel")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblChannel = (Label)gvChannel.Rows[index].FindControl("lblChannel");
                objBO.Channel =  lblChannel.Text.Trim();

                Session["ChannelID"] = objBO.ChannelID;
                Session["Channel"] = objBO.Channel;
                Response.Redirect("UpdateChannel.aspx");
            }
            else if (e.CommandName == "DeleteChannel")
            {
                ChannelBAL objBAL = new ChannelBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteChannel(objBO.ChannelID, objBO.DeletedBy);
                GetChannel();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Grid View Page Index Changing Event

    protected void gvChannel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChannel.PageIndex = e.NewPageIndex;
        GetChannel();
    }

    #endregion
    
}
