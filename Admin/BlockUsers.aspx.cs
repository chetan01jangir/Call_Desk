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

public partial class Admin_BlockUser : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
    }
    #endregion

    #region Bind Funtion Call

    public void GetBlockUserDetails()
    {
        UserBAL objBAL = new UserBAL();
        string strUserName = txtUserName.Text.Trim();

        gvBlockUser.DataSource = objBAL.GetBlockUserDetails(strUserName);
        gvBlockUser.DataBind();
    }

    #endregion

    #region Code For Button Submit

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetBlockUserDetails();
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid Row Command Event

    protected void gvBlockUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "BlockUser")
            {
                ApproverAuthorityBAL objABAL = new ApproverAuthorityBAL();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblUserCode = (Label)gvBlockUser.Rows[index].FindControl("lblUserCode");
                string strApprover;
                strApprover = objABAL.CheckApproverForAIRSTMapping(lblUserCode.Text);
                if (strApprover == "1")
                {

                    UserBAL objBAL = new UserBAL();
                    objBAL.BlockUser(lblUserCode.Text);
                    lblMessage.Text = "User Name " + lblUserCode.Text + " " + "has been Blocked successfully...";
                    GetBlockUserDetails();
                }
                else
                {
                    lblMessage.Text = "Approver " + '"' + lblUserCode.Text + '"' + " exists for mapping";
                }
            }
            else if (e.CommandName == "UnBlockUser")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblUserCode = (Label)gvBlockUser.Rows[index].FindControl("lblUserCode");

                UserBAL objBAL = new UserBAL();
                objBAL.UnBlockUser(lblUserCode.Text);
                lblMessage.Text = "User Name " + lblUserCode.Text + " " + "has been UnBlocked successfully...";
                GetBlockUserDetails();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid Row Data Bound Event

    protected void gvBlockUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblUserStatus = (Label)e.Row.FindControl("lblUserStatus");
                LinkButton lnkBlock = (LinkButton)e.Row.FindControl("lnkBlock");
                LinkButton lnkUnBlock = (LinkButton)e.Row.FindControl("lnkUnBlock");
                if (lblUserStatus.Text.ToUpper() == "TRUE")
                {
                    lnkUnBlock.Enabled = false;
                }
                else
                {
                    lnkBlock.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion


}
