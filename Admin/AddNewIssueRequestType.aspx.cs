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
using AjaxControlToolkit;

public partial class AddIssueRequestType : System.Web.UI.Page
{

    private int intCnt;
    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");
        if (!IsPostBack)
        {
            GetIssueRequestType();
        }
    }

    #endregion

    #region Get Issue Request Type

    public void GetIssueRequestType()
    {
        try
        {
            lblMessage.Text = "";
            IssueRequestTypeBAL objBAL = new IssueRequestTypeBAL();
            gvIssueRequestType.DataSource = objBAL.GetIssueRequestType();
            gvIssueRequestType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Submit Button Code

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            IssueRequestTypeBAL objBAL = new IssueRequestTypeBAL();
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.IssueRequestType = txtIssueRequestType.Text.Trim();
            objAdminBO.CreatedBy = Membership.GetUser().UserName;

            if (objBAL.CheckExistingIssueRequestType(objAdminBO.IssueRequestType) == "1")
            {
                lblMessage.Text = "Issue request type already exists.....";
            }
            else
            {
                int intReturnVal = objBAL.AddNewIssueRequestType(objAdminBO.IssueRequestType, objAdminBO.CreatedBy);
                GetIssueRequestType();
                lblMessage.Text = "Issue request type added successfully.....";
                txtIssueRequestType.Text = "";
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
            Response.Redirect("../Default.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Issue Request Type Page Index Changing Event

    protected void gvIssueRequestType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvIssueRequestType.PageIndex = e.NewPageIndex;
            GetIssueRequestType();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Grid View Issue Request Type Row Command Event

    protected void gvIssueRequestType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.IssueRequestID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditIssueRequest")
            {
                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;

                Label lblIssueRequestType = (Label)gvIssueRequestType.Rows[index].FindControl("lblIssueRequestType");
                objAdminBO.IssueRequestType = lblIssueRequestType.Text;

                Session["IssueRequestTypeID"] = objAdminBO.IssueRequestID;
                Session["IssueRequestType"] = objAdminBO.IssueRequestType;

                Response.Redirect("UpdateIssueRequestType.aspx");
            }
            else if (e.CommandName == "DeleteIssueRequest")
            {
                objAdminBO.DeletedBy = Membership.GetUser().UserName;

                IssueRequestTypeBAL objBAL = new IssueRequestTypeBAL();
                int intReturnVal = objBAL.DeleteIssueRequestType(objAdminBO.IssueRequestID, objAdminBO.DeletedBy);

                if (intReturnVal == 1)
                {
                    GetIssueRequestType();
                    lblMessage.Text = "Issue request type deleted successfully.....";
                }
                else
                {
                    lblMessage.Text = "Issue request type can not be deleted as mapping exists with application";       
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

    #region Grid View Issue Request Row Data Bound Event

    protected void gvIssueRequestType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            intCnt = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            intCnt++;
            ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
            ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkDelete");
            objModalPopupExtender.BehaviorID = "mdlPopup" + intCnt.ToString();
            objLinkButton.OnClientClick = "showConfirm(this, 'mdlPopup" + intCnt.ToString() + "'); return false;";
        }
    }

    #endregion   

    #region Search Issue Request Type Like

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            IssueRequestTypeBAL objBAL = new IssueRequestTypeBAL();
            gvIssueRequestType.DataSource = objBAL.SearchIssueRequestType(txtSearchIRT.Text);
            gvIssueRequestType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}
