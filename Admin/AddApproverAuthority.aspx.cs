using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBO;
using CallDeskBAL;
using AjaxControlToolkit;

public partial class ApproverMaster : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load Code
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetApproverAuthority();
        }
    }
    #endregion

    #region Submit Button Code
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            objAdminBO.ApproverAuthority = txtApproverAuthority.Text.Trim();
            objAdminBO.CreatedBy = Membership.GetUser().UserName;

            if (objBAL.CheckExistingApproverAuthority(objAdminBO.ApproverAuthority) == "1")
            {
                lblMessage.Text = "Approver Authority already exists......";
            }
            else
            {
                int i = AddNewApproverAuthority(objAdminBO);
                GetApproverAuthority();
                lblMessage.Text = "Approver Authority Added Successfully......";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Method to Add New Approver Authority Code
    public int AddNewApproverAuthority(AdminBO objAdminBO)
    {
        int i = 0;
        ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
        i = objBAL.AddNewApproverAuthority(objAdminBO.ApproverAuthority, objAdminBO.CreatedBy);
        return i;
    }
    #endregion

    #region Cancel Button Code
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Method which get the Approver Authorities
    public void GetApproverAuthority()
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            gvApproverAuthority.DataSource = objBAL.GetApproverAuthority();
            gvApproverAuthority.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }
    #endregion

    #region Approver Authority Page Index Change Event
    protected void gvApproverAuthority_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvApproverAuthority.PageIndex = e.NewPageIndex;
            GetApproverAuthority();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Approver Authority Row Command Event
    protected void gvApproverAuthority_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.ApproverAuthorityID = int.Parse((e.CommandArgument).ToString());


            if (e.CommandName == "EditApprover")
            {
                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;

                Label lblApproverAuthority = (Label)gvApproverAuthority.Rows[index].FindControl("lblApproverAuthority");
                objAdminBO.ApproverAuthority = lblApproverAuthority.Text;

                Session["ApproverAuthorityID"] = objAdminBO.ApproverAuthorityID;
                Session["ApproverAuthorityName"] = objAdminBO.ApproverAuthority;

                Response.Redirect("UpdateApproverAuthority.aspx");
            }
            else if (e.CommandName == "DeleteApprover")
            {
                ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();

                objAdminBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteApproverAuthority(objAdminBO.ApproverAuthorityID, objAdminBO.DeletedBy);
                GetApproverAuthority();
                lblMessage.Text = "Approver Authority Deleted Successfully......";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Approver Authority Grid Row Data Bound

    protected void gvApproverAuthority_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            intCnt = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            intCnt++;
            ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
            ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkDelete");
            objModalPopupExtender.BehaviorID = "mdlPopup" + intCnt.ToString();
            objLinkButton.OnClientClick = "showConfirm(this, 'mdlPopup" + intCnt.ToString() + "'); return false;";
        }
    }

    #endregion
    
}
