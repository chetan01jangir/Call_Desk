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

public partial class AddApplication : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");
        if (!IsPostBack)
        {
            GetApplicationType();
        }
    }

    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            AdminBAL objAdminBAL = new AdminBAL();
            ApplicationBAL objBAL = new ApplicationBAL();
            objAdminBO.ApplicationName = txtApplicationName.Text.Trim();
            objAdminBO.CreatedBy = Membership.GetUser().UserName;

            if (objBAL.CheckExistingApplication(objAdminBO.ApplicationName) == "1")
            {
                lblMessage.Text = "Application Name already exists......";
            }
            else
            {
                int i = AddNewApplication(objAdminBO);
                GetApplicationType();
                lblMessage.Text = "Application Name Added Successfully......";
                txtApplicationName.Text = "";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
        //Response.Write("<script language=javascript>alert('Created new Call for Application :- " + objBO.ApplicationName + "');top.location=\"../Default.aspx\";</script>");
    }

    #endregion

    #region Method to Add New Application Code

    public int AddNewApplication(AdminBO objAdminBO)
    {
        ApplicationBAL objBAL = new ApplicationBAL();
        int i = 0;

        i = objBAL.AddNewApplication(objAdminBO.ApplicationName, objAdminBO.CreatedBy);

        return i;
    }

    #endregion

    #region Method which get the Application

    public void GetApplicationType()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            gvApplication.DataSource = objBAL.GetApplicationType();
            gvApplication.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Grid Page Index Change Event

    protected void gvApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvApplication.PageIndex = e.NewPageIndex;
            GetApplicationType();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Grid Row Command Event

    protected void gvApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            AdminBO objAdminBO = new AdminBO();
            objAdminBO.ApplicationID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditApplication")
            {
                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;
                Label lblApplicationName = (Label)gvApplication.Rows[index].FindControl("lblApplicationName");
                objAdminBO.ApplicationName = lblApplicationName.Text;
                Session["ApplicationID"] = objAdminBO.ApplicationID;
                Session["ApplicationName"] = objAdminBO.ApplicationName;
                Response.Redirect("UpdateApplication.aspx");
            }
            else if (e.CommandName == "DeleteApplication")
            {
                ApplicationBAL objApplicationBAL = new ApplicationBAL();

                objAdminBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objApplicationBAL.DeleteApplication(objAdminBO.ApplicationID, objAdminBO.DeletedBy);
                if (intReturnValue == 1)
                {
                    GetApplicationType();
                    lblMessage.Text = "Application deleted successfully......";
                }
                else
                {
                    lblMessage.Text = "Application can not be deleted as its mapping with issue request exists";
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

    #region Application Grid Row Data Bound Event

    protected void gvApplication_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region Search Application Name Like Button Code

    protected void btnApplicationName_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            ApplicationBAL objBAL = new ApplicationBAL();
            gvApplication.DataSource = objBAL.SearchApplicationName(txtSearchApplicationName.Text);
            gvApplication.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
}

