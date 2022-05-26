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
using System.IO;
using System.Xml;
using System.Collections.Generic;
using CallDeskBAL;
using AjaxControlToolkit;
using CallDeskBO;

public partial class Admin_UserBranchMapping : System.Web.UI.Page
{

    private int intCnt;
    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            WucSearchmoreUserMapping1.TextboxName = txtUserCode.ClientID;
        }
    }

    #endregion

    #region Save Button Code

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            string strUserName, strBranchCode, strCreatedBy;

            strUserName = txtUserCode.Text.Trim();
            strBranchCode = ddlBranch.SelectedValue;
            strCreatedBy = Membership.GetUser().UserName;

            UserRoleBAL objBAL = new UserRoleBAL();            

            string strReturnValue = objBAL.CheckExistingMember(strUserName);

            if (strReturnValue == "1")
            {
                if (objEMBAL.CheckUserBranchMappingExisting(strUserName, strBranchCode) == "1")
                {
                    lblError.Text = "Mapping exists";
                }
                else
                {
                    int i = objEMBAL.AddEmployeeBranchMapping(strUserName, strBranchCode, strCreatedBy);
                    GetEmployeeMappedToBranches(strUserName);
                    ccdZone.SelectedValue = "Select Zone";
                    lblError.Text = "Mapping Created";
                }
            }
            else
            {
                lblError.Text = "User does not exists";
            }            
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Search Employee Code

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetEmployeeMappedToBranches(txtUserCode.Text.Trim());
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Get Employee Mapped to Branches Code

    public void GetEmployeeMappedToBranches(string strEmployeeCode)
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            gvUserBranchMapping.DataSource = objEMBAL.GetEmployeeMappedToBranches(strEmployeeCode);
            gvUserBranchMapping.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Grid View Page Index Changing Event

    protected void gvUserBranchMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUserBranchMapping.PageIndex = e.NewPageIndex;
            GetEmployeeMappedToBranches(txtUserCode.Text);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Grid View Row Command Event

    protected void gvUserBranchMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            string strUserName = Membership.GetUser().UserName;

            if (e.CommandName == "lnkEdit")
            {
                DataSet dsUserDetails = new DataSet();
                int UserBranchMappingID;
                UserBranchMappingID = int.Parse(e.CommandArgument.ToString());
                dsUserDetails = objEMBAL.EditUserDetailsInUserBranchMapping(UserBranchMappingID);
                ddlZone.SelectedValue = dsUserDetails.Tables[0].Rows[0]["ZoneCode"].ToString();
                ddlRegion.SelectedValue = dsUserDetails.Tables[0].Rows[0]["RegionCode"].ToString();
                ddlBranch.SelectedValue = dsUserDetails.Tables[0].Rows[0]["BranchCode"].ToString();
            }
            else if (e.CommandName == "lnkDelete")
            {
                int UserBranchMappingID;

                UserBranchMappingID = int.Parse(e.CommandArgument.ToString());

                int intReturnVal = objEMBAL.DeleteUserInBranch(UserBranchMappingID, strUserName);
                if (intReturnVal == 1)
                {
                    GetEmployeeMappedToBranches(txtUserCode.Text);
                }
            }
            else if (e.CommandName == "lnkDefault")
            {
                int UserBranchMappingID = int.Parse(e.CommandArgument.ToString());
                int intReturnVal = objEMBAL.SetDefaultUserBranch(UserBranchMappingID, txtUserCode.Text, strUserName);

                GetEmployeeMappedToBranches(txtUserCode.Text);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Row Data Bound Event

    protected void gvUserBranchMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strDefault = e.Row.Cells[4].Text;
                if (strDefault == "True")
                {
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("lnkbtnDelete");
                    ImageButton lnkDefault = (ImageButton)e.Row.FindControl("lnkDefault");

                    lnkDelete.Visible = false;
                    lnkDelete.OnClientClick = "";
                    lnkDefault.Visible = false;
                    lnkDefault.OnClientClick = "";
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
                intCnt = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                intCnt++;
                ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
                ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkbtnDelete");
                objModalPopupExtender.BehaviorID = "mdlPopup" + intCnt.ToString();
                objLinkButton.OnClientClick = "showConfirm(this, 'mdlPopup" + intCnt.ToString() + "'); return false;";

                ModalPopupExtender objModalPopupExtender1 = (ModalPopupExtender)e.Row.FindControl("md2");
                ImageButton objLinkButton1 = (ImageButton)e.Row.FindControl("lnkDefault");
                objModalPopupExtender1.BehaviorID = "mdlPopups" + intCnt.ToString();
                objLinkButton1.OnClientClick = "showConfirm(this, 'mdlPopups" + intCnt.ToString() + "'); return false;";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion

    #region Clear Controls

    public void ClearControls()
    {        
        ccdZone.SelectedValue = "Select Zone";
    }

    #endregion
}
