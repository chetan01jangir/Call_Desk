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

public partial class Admin_UpdateRoles : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfRoleID.Value = Convert.ToString(Session["RoleID"]);
            hfRole.Value = Convert.ToString(Session["Role"]);
            txtRoles.Text = hfRole.Value;
        }

    }

    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            UserRoleBO objBO = new UserRoleBO();
            UserRoleBAL objBAL = new UserRoleBAL();
            objBO.RoleID = new Guid(Session["RoleID"].ToString());
            objBO.Role = txtRoles.Text.Trim();

            if (txtRoles.Text.Trim() == hfRole.Value)
            {
                Response.Redirect("AddRoles.aspx");
            }
            else
            {
                int intReturnValue = objBAL.UpdateRole(objBO.RoleID, objBO.Role);
                if (intReturnValue == 1)
                {
                    Response.Redirect("AddRoles.aspx");
                }
                else
                {
                    lblMessage.Text = "Role already exists.";
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

    #region Code Button Cancel

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRoles.aspx");
    }

    #endregion
    
}
