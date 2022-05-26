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

public partial class Admin_ZoneMaster : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetZone();
        }
    }

    #endregion

    #region Method to Get Zone

    public void GetZone()
    {
        LocationBAL objBAL = new LocationBAL();
        gvZone.DataSource = objBAL.GetZoneName();
        gvZone.DataBind();
    }

    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            LocationBAL objBAL = new LocationBAL();
            objBO.CreatedBy = Membership.GetUser().UserName;
            objBO.ZoneName = txtZone.Text.Trim();
            objBO.ZoneCode = txtZoneCode.Text.Trim();

            if (objBAL.CheckExistingZone(objBO.ZoneName, objBO.ZoneCode) == "0")
            {
                lblMessage.Text = "Zone Name or Zone Code already exists.....";
            }
            else
            {
                int intReturnValue = objBAL.AddNewZone(objBO.ZoneName, objBO.ZoneCode, objBO.CreatedBy);
                GetZone();
                lblMessage.Text = "Zone added successfully.....";
            }
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
        Response.Redirect("Default.aspx");
    }

    #endregion

    #region Grid View Row Command Event

    protected void gvZone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LocationBO objBO = new LocationBO();
        objBO.ZoneID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditZone")
        {
        }
        else if (e.CommandName == "DeleteZone")
        {
            LocationBAL objBAL = new LocationBAL();
            objBO.DeletedBy = Membership.GetUser().UserName;
            int intReturnValue = objBAL.DeleteZone(objBO.ZoneID, objBO.DeletedBy);
            GetZone();
            lblMessage.Text = "Zone delete successfully.....";
        }
    }
    
    #endregion

    #region Grid View Row Data Bound Event

    protected void gvZone_RowDataBound(object sender, GridViewRowEventArgs e)
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
