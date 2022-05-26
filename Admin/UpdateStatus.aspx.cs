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

public partial class Admin_UpdateStatus : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfApproverAuthorityID.Value = Convert.ToString(Session["StatusID"]);
            hfApproverAuthority.Value = Convert.ToString(Session["Status"]);
            txtStatus.Text = hfApproverAuthority.Value;
        }
    }

    #endregion

    #region Code Button Update

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            StatusBO objBO = new StatusBO();
            StatusBAL objBAL = new StatusBAL();
            objBO.StatusID = Convert.ToInt32(hfApproverAuthorityID.Value);
            objBO.Status = txtStatus.Text;
            objBO.UpdatedBy = Membership.GetUser().UserName;
            int intReturnValue = objBAL.UpdateStatus(objBO.StatusID, objBO.Status, objBO.UpdatedBy);
            Response.Redirect("AddStatus.aspx");
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
        try
        {
            Response.Redirect("AddStatus.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}
