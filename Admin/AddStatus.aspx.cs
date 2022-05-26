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

public partial class Admin_AddStatus : System.Web.UI.Page
{
    private int intCnt;

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            GetStatus();
        }
    }
    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StatusBO objBO = new StatusBO();
            StatusBAL objBAL = new StatusBAL();
            objBO.Status = txtStatus.Text;

            if (objBAL.CheckStatusExisting(objBO.Status) == "0")
            {
                lblMessage.Text = "Status already exists......";
            }
            else
            {
                objBO.CreatedBy = Membership.GetUser().UserName;
                objBO.Status = txtStatus.Text.ToString();
                int i = AddNewStatus(objBO);
                GetStatus();
                lblMessage.Text = "Status Added Successfully......";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }

    #endregion

    #region Method to Add New Status

    public int AddNewStatus(StatusBO objBO)
    {
        int i = 0;
        StatusBAL objBAL = new StatusBAL();
        i = objBAL.AddNewStatus(objBO.Status, objBO.CreatedBy);
        return i;
    }
    #endregion

    #region Method which get the Status

    public void GetStatus()
    {
        try
        {
            StatusBAL objStatusBAL = new StatusBAL();
            gvStatus.DataSource = objStatusBAL.GetStatus();
            gvStatus.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }

    #endregion

    #region Status Row Command Event

    protected void gvStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            StatusBO objBO = new StatusBO();
            objBO.StatusID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditStatus")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblStatus = (Label)gvStatus.Rows[index].FindControl("lblStatus");
                objBO.Status = lblStatus.Text;

                Session["StatusID"] = objBO.StatusID;
                Session["Status"] = objBO.Status;

                Response.Redirect("UpdateStatus.aspx");
            }
            else if (e.CommandName == "DeleteStatus")
            {
                StatusBAL objBAL = new StatusBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteStatus(objBO.StatusID, objBO.DeletedBy);
                GetStatus();
                lblMessage.Text = "Status Deleted Successfully.....";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Row Data Bound Event

    protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region Grid View Page Index Changing Event

    protected void gvStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStatus.PageIndex = e.NewPageIndex;
        GetStatus();
    }

    #endregion

    

    
}
