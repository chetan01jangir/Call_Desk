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
using AjaxControlToolkit;

public partial class Admin_AddOfficeType : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");

        if (!IsPostBack)
        {
            GetOfficeTypes();
        }
    }

    #endregion

    #region Get All Office Types

    public void GetOfficeTypes()
    {
        try
        {
            OfficeTypeBAL objBAL = new OfficeTypeBAL();
            gvOfficeTypes.DataSource = objBAL.GetOfficeTypes();
            gvOfficeTypes.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Row Command Event

    protected void gvOfficeTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int intOfficeTypeId = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditOfficeType")
            {
                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;
                Label lblOfficeType = (Label)gvOfficeTypes.Rows[index].FindControl("lblOfficeType");

                Session["OfficeTypeID"] = intOfficeTypeId;
                Session["OfficeType"] = lblOfficeType.Text;
                Response.Redirect("UpdateOfficeType.aspx");
            }
            else if (e.CommandName == "DeleteOfficeType")
            {
                OfficeTypeBAL objBAL = new OfficeTypeBAL();

                string strUserName = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteOfficeType(intOfficeTypeId, strUserName);
                GetOfficeTypes();
                lblMessage.Text = "Office type deleted successfully......";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Row Data Bound Event

    protected void gvOfficeTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Paging Event

    protected void gvOfficeTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvOfficeTypes.PageIndex = e.NewPageIndex;
            GetOfficeTypes();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Add New Office Type

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            OfficeTypeBAL objBAL = new OfficeTypeBAL();
            string strOfficeType, strCreatedBy;
            strOfficeType = txtOfficeType.Text.Trim();
            strCreatedBy = Membership.GetUser().UserName;

            string strReturnVal = objBAL.CheckExistingOfficeType(strOfficeType);
            if (strReturnVal == "0")
            {
                lblMessage.Text = "Office type already exists";
            }
            else
            {
                int intReturnVal = objBAL.AddOfficeType(strOfficeType, strCreatedBy);
                GetOfficeTypes();
                lblMessage.Text = "Office type addedd successfully....";
                ClearControls();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Search Office Type Like

    protected void btnSearchOfficeType_Click(object sender, EventArgs e)
    {
        try
        {
            OfficeTypeBAL objBAL = new OfficeTypeBAL();
            gvOfficeTypes.DataSource = objBAL.SearchOfficeType(txtSearchOfficeType.Text.Trim());
            gvOfficeTypes.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion 

    #region Clear Controls

    public void ClearControls()
    {
        txtOfficeType.Text = "";
        txtSearchOfficeType.Text = "";
    }

    #endregion
}
