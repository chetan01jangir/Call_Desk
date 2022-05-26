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

public partial class Admin_UpdateDesignation : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            try
            {
                hfDesignationID.Value = Convert.ToString(Session["DesignationID"]);
                hfDesignation.Value = Convert.ToString(Session["Designation"]);
                txtDesignation.Text = hfDesignation.Value;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
				lblMessage.Text = "Error has occurred please contact the administrator.";
            }
        }
    }

    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objBO = new AdminBO();
            DesignationBAL objBAL = new DesignationBAL();
            objBO.DesignationID = Convert.ToInt32(hfDesignationID.Value);
            objBO.Designation = txtDesignation.Text.Trim();
            objBO.UpdatedBy = Membership.GetUser().UserName;
            int intReturnValue = objBAL.UpdateDesignation(objBO.DesignationID, objBO.Designation, objBO.UpdatedBy);
            Response.Redirect("AddDesignation.aspx");
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
        Response.Redirect("AddDesignation.aspx");
    }

    #endregion
    
}
