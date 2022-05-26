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

public partial class Admin_UpdateIssueRequestSubType : System.Web.UI.Page
{
    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfIssueRequestSubTypeID.Value = Convert.ToString(Session["IssueRequestSubTypeID"]);
            hfIssueRequestSubType.Value = Convert.ToString(Session["IssueRequestSubType"]);
            txtIssueRequestSubType.Text = hfIssueRequestSubType.Value;
        }
    }

    #endregion

    #region Update Button Code for Issue Request SubType

    protected void btnUpdateIssueRequestSubType_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            IssueRequestSubTypeBAL objBAL = new IssueRequestSubTypeBAL();

            objAdminBO.IssueRequestSubTypeID = Convert.ToInt32(hfIssueRequestSubTypeID.Value);
            objAdminBO.IssueRequestSubType = txtIssueRequestSubType.Text.Trim();
            objAdminBO.UpdatedBy = Membership.GetUser().UserName;

            if (txtIssueRequestSubType.Text.Trim() == hfIssueRequestSubType.Value)
            {
                Response.Redirect("AddNewIssueRequestSubType.aspx");
            }
            else
            {
                int intReturnVal = objBAL.UpdateIssueRequestSubType(objAdminBO.IssueRequestSubTypeID, objAdminBO.IssueRequestSubType, objAdminBO.UpdatedBy);

                if (intReturnVal == 1)
                {
                    Response.Redirect("AddNewIssueRequestSubType.aspx");
                }
                else
                {
                    lblMessage.Text = "Issue request sub type alrady exists.";
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

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddNewIssueRequestSubType.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
}
