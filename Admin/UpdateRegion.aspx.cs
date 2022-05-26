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

public partial class Admin_UpdateRegion : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            try
            {
                BindZone();
                hfZoneID.Value = Convert.ToString(Session["ZoneId"]);
                hfRegionID.Value = Session["RegionID"].ToString();
                hfRegionCode.Value = Convert.ToString(Session["RegionCode"]);
                hfRegionName.Value = Convert.ToString(Session["RegionName"]);
                ddlZone.SelectedValue = hfZoneID.Value;
                txtRegion.Text = hfRegionName.Value;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
				lblMessage.Text = "Error has occurred please contact the administrator.";

            }            
        }
    }

    #endregion

    #region Method to Get Zone

    public void BindZone()
    {
        UserRoleBAL objBAL = new UserRoleBAL();        
        ddlZone.DataSource = objBAL.GetZone();
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataValueField = "ZoneID_PK";
        ddlZone.DataBind();
        CommonUtility.AddSelectToDropDown(ddlZone);
    }

    #endregion

    #region Button Submit Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            LocationBAL objBAL = new LocationBAL();
            objBO.RegionID = Convert.ToInt32(hfRegionID.Value);
            string strZoneId = ddlZone.SelectedValue;
            objBO.RegionName = txtRegion.Text.Trim();
            objBO.UpdatedBy = Membership.GetUser().UserName;
            if (objBAL.CheckRegionNameForUpdation(objBO.RegionName) == "0")
            {
                lblMessage.Text = "Region Name " + '"' + objBO.RegionName + '"' + " already exists.";
            }
            else
            {
                int intReturnValue = objBAL.UpdateRegion(objBO.RegionID, strZoneId, objBO.RegionName, objBO.UpdatedBy);
                Response.Redirect("RegionMaster.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Button Cancel Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegionMaster.aspx");
    }

    #endregion

}
