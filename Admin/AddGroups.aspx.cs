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

public partial class Admin_AddGroups : System.Web.UI.Page
{
    private int intCnt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        btnCancel.Attributes.Add("onclick", "return ClearControl();");
        if (!IsPostBack)
        {
            GetGroups();
        }
    }
    #endregion

    #region Code Button Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GroupsBO objBO = new GroupsBO();
            GroupsBAL objBAL = new GroupsBAL();
            objBO.Groups = txtGroups.Text.Trim();

            if (objBAL.CheckGroups(objBO.Groups) == "0")
            {
                lblMessage.Text = "Group already exists......";
            }
            else
            {
                objBO.CreatedBy = Membership.GetUser().UserName;
                objBO.Groups = txtGroups.Text.Trim();
                objBAL.AddNewGroup(objBO.Groups, objBO.CreatedBy);
                lblMessage.Text = "Group " + objBO.Groups + " added succesfully.....";
                GetGroups();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Method to Bind Grid

    public void GetGroups()
    {
        try
        {
            GroupsBAL objBAL = new GroupsBAL();
            gvGroups.DataSource = objBAL.GetGroups();
            gvGroups.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid View Row Command Event

    protected void gvGroups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GroupsBO objBO = new GroupsBO();
            objBO.GroupsID = int.Parse((e.CommandArgument).ToString());

            if (e.CommandName == "EditGroups")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gvr.RowIndex;
                Label lblGroups = (Label)gvGroups.Rows[index].FindControl("lblGroups");
                objBO.Groups = lblGroups.Text;
                Session["GroupId"] = objBO.GroupsID;
                Session["Groups"] = objBO.Groups;
                Response.Redirect("UpdateGroups.aspx");
            }
            else if (e.CommandName == "DeleteGroups")
            {
                GroupsBAL objBAL = new GroupsBAL();
                objBO.DeletedBy = Membership.GetUser().UserName;
                objBAL.DeleteGroups(objBO.GroupsID, objBO.DeletedBy);
                GetGroups();
                lblMessage.Text = "Group Deleted Successfully....";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Grid Row Data Bound Event

    protected void gvGroups_RowDataBound(object sender, GridViewRowEventArgs e)
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
    
    
    #region Grid Row Data Bound Event

    protected void gvGroups_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvGroups.PageIndex = e.NewPageIndex;
            GetGroups();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;       
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }        
    }

    #endregion
}
