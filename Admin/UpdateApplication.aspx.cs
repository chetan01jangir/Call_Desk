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

public partial class UpdateIssueRequestMaster : System.Web.UI.Page
{

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            try
            {
                hfApplicationID.Value = Session["ApplicationID"].ToString();
                hfApplicationName.Value = Session["ApplicationName"].ToString();
                txtApplicationName.Text = hfApplicationName.Value;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
				lblMessage.Text = "Error has occurred please contact the administrator.";
            }
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddNewApplication.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }

    #endregion

    #region Update Button Code

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ApplicationBAL objBAL = new ApplicationBAL();
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.ApplicationID = int.Parse(hfApplicationID.Value);
            objAdminBO.ApplicationName = txtApplicationName.Text.Trim();
            objAdminBO.UpdatedBy = Membership.GetUser().UserName;

            if (txtApplicationName.Text.Trim() == hfApplicationName.Value)
            {
                Response.Redirect("AddNewApplication.aspx");                
            }
            else
            {
                int intReturnVal = objBAL.UpdateApplication(objAdminBO.ApplicationID, objAdminBO.ApplicationName, objAdminBO.UpdatedBy);

                if (intReturnVal == 1)
                {
                    Response.Redirect("AddNewApplication.aspx");
                }
                else
                {
                    lblMessage.Text = "Application name already exists.";
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
