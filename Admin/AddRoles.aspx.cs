using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBO;
using CallDeskBAL;
using AjaxControlToolkit;

public partial class Admin_AddRoles : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");
        if (!IsPostBack)
        {
            GetRole();
        }
    }

    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            UserRoleBO objBO = new UserRoleBO();
            objBO.Role = txtRole.Text.Trim();
            objBO.ApplicationName = "CallDeskOnline";
            UserRoleBAL objBAL = new UserRoleBAL();
            int i = objBAL.AddNewRole(objBO.ApplicationName, objBO.Role);
            if (i == 1)
            {
                lblMessage.Text = "Role added Successfully...";
                GetRole();
                txtRole.Text = "";
            }
            else
            {
                lblMessage.Text = "Role already exists....";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";

        }
    }

    #endregion

    #region Method to Get Role

    public void GetRole()
    {
        UserRoleBAL objBAL = new UserRoleBAL();
        DataSet dsRoles = new DataSet();
        dsRoles = objBAL.GetRole();
        gvRole.DataSource = dsRoles;
        gvRole.DataBind();
    }

    #endregion

    #region gvRole_RowCommand

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            UserRoleBO objBO = new UserRoleBO();
                        
            if (e.CommandName == "EditRole")
            {
                objBO.RoleID = new Guid(e.CommandArgument.ToString());
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblRole = (Label)gvRole.Rows[index].FindControl("lblRole");
                objBO.Role = lblRole.Text;
                Session["RoleID"] = objBO.RoleID;
                Session["Role"] = objBO.Role;
                Response.Redirect("UpdateRoles.aspx");
            }
            else if (e.CommandName == "DeleteRole")
            {
                objBO.RoleID = new Guid(e.CommandArgument.ToString());
                UserRoleBAL objBAL = new UserRoleBAL();
                int intReturnValue = objBAL.DeleteRole(objBO.RoleID);
                if (intReturnValue == 1)
                {
                    GetRole();
                    lblMessage.Text = "Role deleted Successfully...";
                }
                else
                {
                    lblMessage.Text = "Role cannot be deleted as user in that role exists.";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";

        }
        finally
        {

        }
    }

    #endregion

    #region Grid View Page Index Changing Event

    protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvRole.PageIndex = e.NewPageIndex;
            GetRole();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";

        }        
    }

    #endregion

    #region Row Data Bound Code

    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region Search Button Code

    protected void btnSearchRole_Click(object sender, EventArgs e)
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            gvRole.DataSource = objBAL.SearchRoleLike(txtRoleType.Text.Trim());
            gvRole.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";

        }
    }

    #endregion
}
