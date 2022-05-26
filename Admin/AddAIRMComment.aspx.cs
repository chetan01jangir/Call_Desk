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

public partial class Admin_AddAIRMComment : System.Web.UI.Page
{
    #region Page Load Code
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetApplications();
        }
    }
    #endregion

    #region Get Application
    public void GetApplications()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            ddlApplications.DataSource = objBAL.GetApplicationType();
            ddlApplications.DataTextField = "ApplicationName";
            ddlApplications.DataValueField = "ApplicationID_PK";
            ddlApplications.DataBind();
            CommonUtility.AddSelectToDropDown(ddlApplications);
            CommonUtility.AddSelectToDropDown(ddlIssueRequestTypes);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Button Click event to Add Comment
    protected void btnAddComment_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            AdminBAL objAdminBAL = new AdminBAL();
            objAdminBO.ApplicationID = int.Parse(ddlApplications.SelectedValue);
            objAdminBO.IssueRequestID = int.Parse(ddlIssueRequestTypes.SelectedValue);
            objAdminBO.Comment = txtComment.Text;
            objAdminBAL.AddApplicationIssueRequestMappingComment(objAdminBO);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Application dropdown Selected Index Change Code
    protected void ddlApplications_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            ddlIssueRequestTypes.DataSource = objBAL.GetIssueRequestTypeByApplicationType(int.Parse(ddlApplications.SelectedValue));
            ddlIssueRequestTypes.DataTextField = "IssueRequestType";
            ddlIssueRequestTypes.DataValueField = "IssueRequestType_PK";
            ddlIssueRequestTypes.DataBind();
            CommonUtility.AddSelectToDropDown(ddlIssueRequestTypes);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion
    
}
