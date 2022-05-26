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
using CallDeskBAL;
using CallDeskBO;

public partial class UpdateApproverMaster : System.Web.UI.Page
{
    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfApproverAuthorityID.Value = Session["ApproverAuthorityID"].ToString();
            hfApproverAuthority.Value = Session["ApproverAuthorityName"].ToString();

            txtApproverAuthority.Text = hfApproverAuthority.Value;
        }
    }

    #endregion
        
    #region Cancel Button Code
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Update Button Code
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.ApproverAuthorityID = int.Parse(hfApproverAuthorityID.Value);
            objAdminBO.ApproverAuthority = txtApproverAuthority.Text;
            objAdminBO.UpdatedBy = Membership.GetUser().UserName;

            objBAL.UpdateApproverAuthority(objAdminBO.ApproverAuthorityID, objAdminBO.ApproverAuthority, objAdminBO.UpdatedBy);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion
}
