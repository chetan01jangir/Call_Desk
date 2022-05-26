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

public partial class Admin_AddDesignation : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetDesignation();
        }
    }

    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBO objBO = new AdminBO();            

            DesignationBAL objBAL = new DesignationBAL();
            objBO.Designation = txtDesignation.Text.Trim();

            if (objBAL.CheckExistingDesignation(objBO.Designation) == "0")
            {
                lblMessage.Text = "Designation already exists.....";
            }
            else
            {
                objBO.CreatedBy = Membership.GetUser().UserName;
                objBO.Designation = txtDesignation.Text.Trim();
                int i = objBAL.AddNewDesignation(objBO.Designation, objBO.CreatedBy);
                GetDesignation();
                lblMessage.Text = "Designation Added Successfully.....";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }

    }

    #endregion

    #region Method to get the Designation

    public void GetDesignation()
    {
        try
        {
            DesignationBAL objBAL = new DesignationBAL();
            gvDesignation.DataSource = objBAL.GetDesignation();
            gvDesignation.DataBind();
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
        Response.Redirect("../Default.aspx");
    }

    #endregion

    #region Designation Grid Row Command event

    protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            AdminBO objBO = new AdminBO();
            objBO.DesignationID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditDesignation")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblDesignation = (Label)gvDesignation.Rows[index].FindControl("lblDesignation");
                objBO.Designation = lblDesignation.Text;

                Session["DesignationID"] = objBO.DesignationID;
                Session["Designation"] = objBO.Designation;
                Response.Redirect("UpdateDesignation.aspx");
            }
            else if (e.CommandName == "DeleteDesignation")
            {
                DesignationBAL objBAL = new DesignationBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                int intReturnValue = objBAL.DeleteDesignation(objBO.DesignationID, objBO.DeletedBy);
                GetDesignation();
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

    protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDesignation.PageIndex = e.NewPageIndex;
        GetDesignation();
    }

    #endregion

    
}
