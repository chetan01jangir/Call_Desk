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

public partial class Admin_UpdateBranch : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            try
            {
                GetLocationType();
                hfBranchID.Value = Session["BranchID"].ToString();
                hfBranchName.Value = Convert.ToString(Session["BranchName"]);
                hfBranchCode.Value = Convert.ToString(Session["BranchCode"]);
                hfOfficeType.Value = Convert.ToString(Session["OfficeType"]);
                txtBranchName.Text = hfBranchName.Value;
                //txtBranchCode.Text = hfBranchCode.Value;
                ddlOfficeType.SelectedValue = hfOfficeType.Value;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
				lblMessage.Text = "Error has occurred please contact the administrator.";
            }

        }
    }

    #endregion

    #region Code Button Update

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            LocationBO objBO = new LocationBO();
            LocationBAL objBAL = new LocationBAL();

            objBO.BranchID = Convert.ToInt32(hfBranchID.Value);
            //objBO.BranchCode = txtBranchCode.Text;
            objBO.BranchName = txtBranchName.Text;
            objBO.OfficeType = ddlOfficeType.SelectedValue;
            objBO.UpdatedBy = Membership.GetUser().UserName;
            if (objBAL.CheckBranchNameForUpdation(objBO.BranchName) == "0")
            {
                lblMessage.Text = "Branch Name already exists.";
            }
            else
            {
                int intReturnValue = objBAL.UpdateBranch(objBO.BranchID, objBO.BranchName, objBO.OfficeType, objBO.UpdatedBy);
                Response.Redirect("BranchMaster.aspx");
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
        Response.Redirect("BranchMaster.aspx");
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

}
