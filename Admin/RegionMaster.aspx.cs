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

public partial class Admin_RegionMaster : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetRegionName();
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
            objBO.CreatedBy = Membership.GetUser().UserName;
            objBO.RegionName = txtRegion.Text.Trim();
            objBO.RegionCode = txtRegionCode.Text;
            objBO.ZoneID = Convert.ToInt32(ddlZone.SelectedItem.Value);

            if (objBAL.CheckExistingRegion(objBO.RegionName, objBO.RegionCode) == "0")
            {
                lblMessage.Text = "Region Name or Region Code already exists.....";
            }
            else
            {
                int intReturnValue = objBAL.AddNewRegion(objBO.RegionName, objBO.RegionCode, objBO.ZoneID, objBO.CreatedBy);
                GetRegionName();
                lblMessage.Text = "Region Added Successfully.....";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }

    #endregion

    #region Method to Get Region

    public void GetRegionName()
    {
        try
        {
            LocationBAL objBAL = new LocationBAL();
            gvRegion.DataSource = objBAL.GetRegionName();
            gvRegion.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }

    #endregion

    #region Grid View Row Command

    protected void gvRegion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            objBO.RegionID = Convert.ToInt32((e.CommandArgument));

            if (e.CommandName == "EditRegion")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblRegionName = (Label)gvRegion.Rows[index].FindControl("lblRegionName");
                Label lblRegionCode = (Label)gvRegion.Rows[index].FindControl("lblRegionCode");
                Label lblZoneName = (Label)gvRegion.Rows[index].FindControl("lblZoneName");
                Label lblZoneId = (Label)gvRegion.Rows[index].FindControl("lblZoneID");
                
                objBO.ZoneName = lblZoneName.Text;
                objBO.RegionName = lblRegionName.Text;
                objBO.RegionCode = lblRegionCode.Text;
                objBO.ZoneID = Convert.ToInt32(lblZoneId.Text);

                Session["ZoneName"] = objBO.ZoneName;
                Session["RegionID"] = objBO.RegionID;
                Session["RegionCode"] = objBO.RegionCode;
                Session["RegionName"] = objBO.RegionName;
                Session["ZoneId"] =  objBO.ZoneID;

                Response.Redirect("UpdateRegion.aspx");
            }
            else if (e.CommandName == "DeleteRegion")
            {
                LocationBAL objBAL = new LocationBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteRegion(objBO.RegionID, objBO.DeletedBy);
                GetRegionName();
                lblMessage.Text = "Region Deleted successfully.....";
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

    protected void gvRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegion.PageIndex = e.NewPageIndex;
        GetRegionName();
    }

    #endregion

    #region Grid View Row Data Bound Event

    protected void gvRegion_RowDataBound(object sender, GridViewRowEventArgs e)
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
