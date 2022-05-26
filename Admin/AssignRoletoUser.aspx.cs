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

public partial class Admin_AssignRoletoUser : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            WucSearchmoreUserMapping1.TextboxName = txtUserCode.ClientID;
            GetRole();
        }
    }
    #endregion

    #region Get Role

    public void GetRole()
    {
        UserRoleBO objBO = new UserRoleBO();
        UserRoleBAL objBAL = new UserRoleBAL();
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleName";
        ddlRole.DataSource = objBAL.GetRole();
        ddlRole.DataBind();
        CommonUtility.AddSelectToDropDown(ddlRole);
    }

    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strUserName, strRole, strApplicationName;

            DateTime strDate;
            UserRoleBAL objBAL = new UserRoleBAL();
            strUserName = txtUserCode.Text.Trim();
            strRole = ddlRole.SelectedValue;            

            string strCheckUser = objBAL.CheckExistingMemberForRoles(strUserName);

            if (strCheckUser == "1")
            {
                string strReturnValue = objBAL.GetRoleID(strRole, strUserName);
                if (strReturnValue == "0")
                {
                    lblMessage.Text = "User is aleady in the Role.";
                    return;
                }
                else if (strReturnValue == "1")
                {
                    lblMessage.Text = "Role added successfully.....";
                    GetRolesforUser();
                    return;
                }
                strApplicationName = Membership.ApplicationName.ToString();
                strDate = System.DateTime.Now;
                objBAL.AddRoleToUser(strApplicationName, strUserName, strRole, strDate);                
                GetRolesforUser();
                lblMessage.Text = "Role added successfully.....";
            }
            else
            {
                lblMessage.Text = "User does not exists";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetRolesforUser();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Roles for User Code

    public void GetRolesforUser()
    {
        try
        {
            lblMessage.Text = "";
            gvRoles.DataSource = null;
            gvRoles.DataBind();

            string strUserName, strApplicationName;

            strUserName = txtUserCode.Text.Trim();
            strApplicationName = Membership.ApplicationName;

            UserRoleBAL objBAL = new UserRoleBAL();
            DataSet dsUserRoles = new DataSet();
            dsUserRoles = objBAL.GetRolesforUser(strApplicationName, strUserName);
            if (dsUserRoles.Tables.Count > 0)
            {
                gvRoles.DataSource = dsUserRoles;
                gvRoles.DataBind();
            }
            else
            {
                lblMessage.Text = "There is no such user...";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Role grid view row command event

    protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strRole, strUserName, strApplicationName;
            strApplicationName = Membership.ApplicationName;
            strUserName = txtUserCode.Text.Trim();
            strRole = Convert.ToString(e.CommandArgument);
            UserRoleBAL objBAL = new UserRoleBAL();

            if (e.CommandName == "lnkDelete")
            {                                                
                int intReturnValue = objBAL.DeleteRoleforUser(strApplicationName, strUserName, strRole);
                GetRolesforUser();
                lblMessage.Text = "Role Deleted Successfully.....";
            }
            if (e.CommandName == "lnkDefault")
            {
                int intReturnVal = objBAL.SetDefaultUserRole(strRole, strUserName);
                if (intReturnVal > 0)
                {
                    GetRolesforUser();
                    lblMessage.Text = "Successfully changes the default role.";
                }
                else
                {
                    lblMessage.Text = "Problem occured while setting the default role.";
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

    #region Grid View Page Index Changing Event

    protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRoles.PageIndex = e.NewPageIndex;
        GetRole();
    }

    #endregion

    #region Grid View Roles Row Data Bound Event

    protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
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

            ModalPopupExtender objModalPopupExtender1 = (ModalPopupExtender)e.Row.FindControl("md2");
            ImageButton objLinkButton1 = (ImageButton)e.Row.FindControl("lnkDefault");
            objModalPopupExtender1.BehaviorID = "mdlPopups" + intCnt.ToString();
            objLinkButton1.OnClientClick = "showConfirm(this, 'mdlPopups" + intCnt.ToString() + "'); return false;";

            string strDefault = e.Row.Cells[2].Text;
            if (strDefault == "True")
            {
                ImageButton lnkDelete = (ImageButton)e.Row.FindControl("lnkDelete");
                ImageButton lnkDefault = (ImageButton)e.Row.FindControl("lnkDefault");

                lnkDelete.Visible = false;
                lnkDelete.OnClientClick = "";
                lnkDefault.Visible = false;
                lnkDefault.OnClientClick = "";
            }
        }
    }

    #endregion

}
