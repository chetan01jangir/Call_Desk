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

public partial class Admin_BranchMasteraspx : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            GetBranch();
            GetLocationType();
        }
    }

    #endregion

    #region Method to Get Branch

    public void GetBranch()
    {
        try
        {
            LocationBAL objBAL = new LocationBAL();
            gvBranch.DataSource = objBAL.GetBranchName();
            gvBranch.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Button Submit Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            LocationBAL objBAL = new LocationBAL();
            objBO.RegionID = Convert.ToInt32(ddlRegion.SelectedValue);
            objBO.BranchName = txtBranchName.Text.Trim();
            objBO.BranchCode = txtBranchCode.Text.Trim();
            objBO.OfficeType = ddlOfficeType.SelectedValue;
            objBO.CreatedBy = Membership.GetUser().UserName;

            if (objBAL.CheckExistingBranch(objBO.BranchName, objBO.BranchCode) == "0")
            {
                lblMessage.Text = "Branch Name or Branch Code already exists.....";
            }
            else
            {
                int intReturnValue = objBAL.AddNewBranch(objBO.BranchName, objBO.BranchCode, objBO.OfficeType, objBO.RegionID, objBO.CreatedBy);
                lblMessage.Text = "Branch Added Successfully.....";
                GetBranch();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View RowCommand

    protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            objBO.BranchID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRegion")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblBranchName = (Label)gvBranch.Rows[index].FindControl("lblBranchName");
                Label lblBranchCode = (Label)gvBranch.Rows[index].FindControl("lblBranchCode");
                Label lblOfficeType = (Label)gvBranch.Rows[index].FindControl("lblOfficeType");
                objBO.BranchName = lblBranchName.Text.Trim();
                objBO.BranchCode = lblBranchCode.Text.Trim();
                objBO.OfficeType = lblOfficeType.Text;

                Session["BranchID"] = objBO.BranchID;
                Session["BranchCode"] = objBO.BranchCode;
                Session["BranchName"] = objBO.BranchName;
                Session["OfficeType"] = objBO.OfficeType;


                Response.Redirect("UpdateBranch.aspx");
            }
            else if (e.CommandName == "DeleteRegion")
            {
                LocationBAL objBAL = new LocationBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteBranch(objBO.BranchID, objBO.DeletedBy);
                GetBranch();
                lblMessage.Text = "Branch Delete Successfully.....";
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

    protected void gvBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBranch.PageIndex = e.NewPageIndex;
        GetBranch();
    }

    #endregion

    #region Get Location Type

    public void GetLocationType()
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            ddlOfficeType.DataSource = objBAL.GetLocationType();
            ddlOfficeType.DataTextField = "Location_Type";
            ddlOfficeType.DataValueField = "Location_Type";
            ddlOfficeType.DataBind();
            CommonUtility.AddSelectToDropDown(ddlOfficeType);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Row Data Bound Event

    protected void gvBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
        
    }
    
    #endregion
    
}
