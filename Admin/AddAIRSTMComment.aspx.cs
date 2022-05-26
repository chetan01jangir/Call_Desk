using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBO;
using CallDeskBAL;

public partial class Admin_AddAIRSTMComment : System.Web.UI.Page
{

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
    }

    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int intReturnValue;
            RegisterNewCallBO objBO = new RegisterNewCallBO();
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            objBO.Description = txtComment.Text.Trim();
            objBO.RowID = Convert.ToInt16(ddlIssueRequestSubType.SelectedValue);
            objBO.UpdatedBy = Membership.GetUser().UserName;
            intReturnValue = objBAL.UpdateComment(objBO.RowID, objBO.Description, objBO.UpdatedBy);
            if (intReturnValue == 1)
            {
                lblMessage.Text = "Comment Added Successfully....";
                ccdApplications.SelectedValue = "Select Application";
                txtComment.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

}
