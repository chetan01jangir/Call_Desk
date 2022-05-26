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
            hfIssueRequestTypeID.Value = Convert.ToString(Session["IssueRequestTypeID"]);
            hfIssueRequestType.Value = Convert.ToString(Session["IssueRequestType"]);

            txtIssueRequestType.Text = hfIssueRequestType.Value;
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddNewIssueRequestType.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Update Button Code

    protected void btnUpdateIssueRequest_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            IssueRequestTypeBAL objBAL = new IssueRequestTypeBAL();

            objAdminBO.IssueRequestID = Convert.ToInt32(hfIssueRequestTypeID.Value);
            objAdminBO.IssueRequestType = txtIssueRequestType.Text.Trim();
            objAdminBO.UpdatedBy = Membership.GetUser().UserName;

            if (txtIssueRequestType.Text.Trim() == hfIssueRequestType.Value)
            {
                Response.Redirect("AddNewIssueRequestType.aspx");                
            }
            else
            {
                int intReturnVal = objBAL.UpdateIssueRequestType(objAdminBO.IssueRequestID, objAdminBO.IssueRequestType, objAdminBO.UpdatedBy);

                if (intReturnVal == 1)
                {
                    Response.Redirect("AddNewIssueRequestType.aspx");
                }
                else
                {
                    lblMessage.Text = "Issue request type alrady exists.";
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
