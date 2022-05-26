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
using AjaxControlToolkit;

public partial class Admin_AddNewIssueRequestSubType : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load Mathod

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");
        if (!IsPostBack)
        {
            GetIssueRequestSubType();
        }
    }

    #endregion

    #region Get Issue Request SubType

    public void GetIssueRequestSubType()
    {
        try
        {
            IssueRequestSubTypeBAL objBAL = new IssueRequestSubTypeBAL();
            gvIssueRequestSubType.DataSource = objBAL.GetIssueRequestSubType();
            gvIssueRequestSubType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Issue Request SubType Grid Page Index Changing Event Code

    protected void gvIssueRequestSubType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvIssueRequestSubType.PageIndex = e.NewPageIndex;
            GetIssueRequestSubType();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Issue Request SubType Grid Row Command Event Code

    protected void gvIssueRequestSubType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.IssueRequestSubTypeID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditIssueRequestSubType")
            {
                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;

                Label lblIssueRequestSubType = (Label)gvIssueRequestSubType.Rows[index].FindControl("lblIssueRequestSubType");
                objAdminBO.IssueRequestSubType = lblIssueRequestSubType.Text;

                Session["IssueRequestSubTypeID"] = objAdminBO.IssueRequestSubTypeID;
                Session["IssueRequestSubType"] = objAdminBO.IssueRequestSubType;

                Response.Redirect("UpdateIssueRequestSubType.aspx");
            }
            else if (e.CommandName == "DeleteIssueRequestSubType")
            {
                objAdminBO.DeletedBy = Membership.GetUser().UserName;

                IssueRequestSubTypeBAL objBAL = new IssueRequestSubTypeBAL();
                int intReturnVal = objBAL.DeleteIssueRequestSubType(objAdminBO.IssueRequestSubTypeID, objAdminBO.DeletedBy);
                if (intReturnVal == 1)
                {
                    GetIssueRequestSubType();
                    lblMessage.Text = "Issue Request Sub Type Deleted Successfully.....";
                }
                else
                {
                    lblMessage.Text = "Can not delete issue Request sub type as the mapping with application and issue request exists";
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

    #region Add New Issue Request Button Code

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            IssueRequestSubTypeBAL objBAL = new IssueRequestSubTypeBAL();
            AdminBO objAdminBO = new AdminBO();

            objAdminBO.IssueRequestSubType = txtIssueRequestSubType.Text.Trim();
            objAdminBO.CreatedBy = Membership.GetUser().UserName;

            if (objBAL.CheckExistingIssueRequestSubType(objAdminBO.IssueRequestSubType) == "1")
            {
                lblMessage.Text = "Issue Request Sub Type already exists.....";
            }
            else
            {
                int intReturnVal = objBAL.AddNewIssueRequestSubType(objAdminBO.IssueRequestSubType, objAdminBO.CreatedBy);
                GetIssueRequestSubType();
                lblMessage.Text = "Issue Request Sub Type Added Successfully.....";
                txtIssueRequestSubType.Text = "";
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

    #region IssueRequestSubType Grid Row Data Bound Event

    protected void gvIssueRequestSubType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region Search Issue Request Sub Type Like

    protected void btnSearchIssueRequestSubType_Click(object sender, EventArgs e)
    {
        try
        {
            IssueRequestSubTypeBAL objBAL = new IssueRequestSubTypeBAL();
            gvIssueRequestSubType.DataSource = objBAL.SearchIssueRequestSubType(txtSearchIssueRequestSubType.Text);
            gvIssueRequestSubType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}
