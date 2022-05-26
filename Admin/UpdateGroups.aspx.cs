using System;
using System.Web;
using CallDeskBO;
using CallDeskBAL;
using System.Data;
using System.Web.UI;
using System.Collections;
using System.Web.Security;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_UpdateGroups : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            txtGroups.Text = Session["Groups"].ToString();
        }
    }
    #endregion

    #region Code Button Submit

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            GroupsBO objBO = new GroupsBO();
            GroupsBAL objBAL = new GroupsBAL();
            objBO.GroupsID = Convert.ToInt32(Session["GroupId"]);
            objBO.Groups = txtGroups.Text.Trim();
            objBO.UpdatedBy = Membership.GetUser().UserName;

            if (txtGroups.Text.Trim() == Convert.ToString(Session["Groups"]))
            {
                Response.Redirect("AddGroups.aspx");
            }
            else
            {                
                int intReturnVal = objBAL.UpdateGroups(objBO.GroupsID, objBO.Groups, objBO.UpdatedBy);

                if (intReturnVal == 1)
                {
                    Response.Redirect("AddGroups.aspx");
                }
                else
                {
                    lblMessage.Text = "Group alrady exists.";
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
            Response.Redirect("AddGroups.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}
