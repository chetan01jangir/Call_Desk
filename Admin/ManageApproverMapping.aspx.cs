using System;
using System.Text;
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
using System.Collections.Generic;
using AjaxControlToolkit;

public partial class Admin_ManageApproverMapping : System.Web.UI.Page
{
    #region Page Level Varaibles

    ArrayList lasset = new ArrayList();
    ArrayList lsubordinate = new ArrayList();
    static ArrayList UpdateList = new ArrayList();

    #endregion

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        ddlType.Attributes.Add("onchange", "SearchCriteria();");        

        if (!IsPostBack)
        {
            BindZone();
        }
    }

    #endregion

    #region Bind Zone Code

    private void BindZone()
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            DataSet dsZone = new DataSet();

            dsZone = objBAL.GetZone();
            lstbxZone.DataSource = dsZone;
            lstbxZone.DataValueField = "ZoneID_PK";
            lstbxZone.DataTextField = "ZoneName";
            lstbxZone.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Add From Issue Request ListBox

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstbxBranch.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstbxBranch.Items.Count; i++)
                {
                    if (lstbxBranch.Items[i].Selected)
                    {
                        if (!lasset.Contains(lstbxBranch.Items[i]))
                        {
                            lasset.Add(lstbxBranch.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lasset.Count; i++)
                {
                    if (!lstbxSelectedBranches.Items.Contains(((ListItem)lasset[i])))
                    {
                        lstbxSelectedBranches.Items.Add(((ListItem)lasset[i]));
                    }
                    lstbxBranch.Items.Remove(((ListItem)lasset[i]));
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

    #region Remove From Issue Request ListBox

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstbxSelectedBranches.SelectedItem != null)
            {
                for (int i = 0; i < lstbxSelectedBranches.Items.Count; i++)
                {
                    if (lstbxSelectedBranches.Items[i].Selected)
                    {
                        if (!lsubordinate.Contains(lstbxSelectedBranches.Items[i]))
                        {
                            lsubordinate.Add(lstbxSelectedBranches.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lsubordinate.Count; i++)
                {
                    if (!lstbxBranch.Items.Contains(((ListItem)lsubordinate[i])))
                    {
                        lstbxBranch.Items.Add(((ListItem)lsubordinate[i]));
                    }
                    lstbxSelectedBranches.Items.Remove(((ListItem)lsubordinate[i]));
                    UpdateList.Add(lsubordinate[i]);
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

    #region Region Selected Index Change

    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strRegionID = ddlRegion.SelectedValue;
    //        AdminBAL objAdminBAL = new AdminBAL();
    //        lstbxBranch.DataSource = objAdminBAL.GetBranchByRegion(strRegionID);
    //        lstbxBranch.DataTextField = "BranchName";
    //        lstbxBranch.DataValueField = "BranchCode";
    //        lstbxBranch.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    #endregion

    #region Add Approver Code

    protected void btnAddApprover_Click(object sender, EventArgs e)
    {
        try
        {
            // lstbxSelectedApprover.Items.Add(new ListItem(txtUserCode.Text, ddlApproverLever.SelectedValue));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Zone List Box Change Event

    protected void lstbxZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstbxBranch.Items.Clear();
            lstbxSelectedBranches.Items.Clear();

            AdminBAL objBAL = new AdminBAL();

            StringBuilder strbldZone = new StringBuilder();

            for (int i = 0; i < lstbxZone.Items.Count; i++)
            {
                if (lstbxZone.Items[i].Selected == true)
                {
                    strbldZone.Append(lstbxZone.Items[i].Value + ",");
                }
            }

            strbldZone.Remove(strbldZone.Length - 1, 1);

            lstbxRegions.DataSource = objBAL.GetRegionByMultipleZone(strbldZone.ToString());
            lstbxRegions.DataTextField = "RegionName";
            lstbxRegions.DataValueField = "RegionID_PK";
            lstbxRegions.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Region change event code

    protected void lstbxRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();

            StringBuilder strbldRegion = new StringBuilder();

            for (int i = 0; i < lstbxRegions.Items.Count; i++)
            {
                if (lstbxRegions.Items[i].Selected == true)
                {
                    strbldRegion.Append(lstbxRegions.Items[i].Value + ",");
                }
            }

            strbldRegion.Remove(strbldRegion.Length - 1, 1);

            lstbxBranch.DataSource = objBAL.GetBranchByMultipleRegion(strbldRegion.ToString());
            lstbxBranch.DataTextField = "BranchName";
            lstbxBranch.DataValueField = "BranchCode";
            lstbxBranch.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Approver Mapping Button Code

    protected void btnGetApprover_Click(object sender, EventArgs e)
    {
        try
        {
            GetApproverMapping();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Approver Mapping Function

    private void GetApproverMapping()
    {
        string strBranch = "0";
        int intAIRST = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);
        StringBuilder strbldBranch = new StringBuilder();

        if (ddlType.SelectedValue == "1") // Branchwise Approver
        {
            for (int i = 0; i < lstbxSelectedBranches.Items.Count; i++)
            {
                if (lstbxSelectedBranches.Items[i].Selected == true)
                {
                    strbldBranch.Append(lstbxSelectedBranches.Items[i].Value + ",");
                }
            }

            strbldBranch.Remove(strbldBranch.Length - 1, 1);
            strBranch = strbldBranch.ToString();
        }

        ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
        gvMappedApprover.DataSource = objBAL.GetAIRSTMappedApprovers(intAIRST, strBranch);
        gvMappedApprover.DataBind();
    }

    #endregion

    #region Service Method to Display data in Panel

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetApproverDetails(string contextKey)
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            DataTable dtLED = new DataTable();
            StringBuilder strbCallDetails = new StringBuilder();
            dtLED = objEMBAL.GetLoggedEmployeeDetails(contextKey);

            if (dtLED.Rows.Count > 0)
            {                
                strbCallDetails.Append("<table style='background-color:#f3f3f3; border: #336699 2px solid; z-index:1000");
                strbCallDetails.Append("width:450px; font-size:11px; font-family:Verdana, Arial, Tahoma, Helvetica, sans-serif;' cellspacing='0' cellpadding='3'>");

                strbCallDetails.Append("<tr><td style='background-color:#1B67A8; color:#ffffff;'>");
                strbCallDetails.Append("<b>Name</b>");
                strbCallDetails.Append("</td>");

                strbCallDetails.Append("<td style='background-color:#1B67A8; color:#ffffff;'>");
                strbCallDetails.Append("<b>Designation</b>");
                strbCallDetails.Append("</td>");

                strbCallDetails.Append("<td style='background-color:#1B67A8; color:#ffffff;'>");
                strbCallDetails.Append("<b>Email</b>");
                strbCallDetails.Append("</td>");

                strbCallDetails.Append("<td style='background-color:#1B67A8; color:#ffffff;'>");
                strbCallDetails.Append("<b>Branch</b>");
                strbCallDetails.Append("</td></tr>");

                strbCallDetails.Append("<tr><td align=" + "left" + ">" + dtLED.Rows[0]["EmployeeName"].ToString() + "</td>");
                strbCallDetails.Append("<td align=" + "left" + ">" + dtLED.Rows[0]["EmployeeDesignation"].ToString() + "</td>");
                strbCallDetails.Append("<td align=" + "left" + ">" + dtLED.Rows[0]["LoweredEmail"].ToString() + "</td>");
                strbCallDetails.Append("<td align=" + "left" + ">" + dtLED.Rows[0]["BranchName"].ToString() + "</td></tr>");

                strbCallDetails.Append("</table>");                
            }
            else
            {
                strbCallDetails.Append("<table style='background-color:#f3f3f3; border: #336699 2px solid; z-index:1000");
                strbCallDetails.Append("width:450px; font-size:11px; font-family:Verdana, Arial, Tahoma, Helvetica, sans-serif;' cellspacing='0' cellpadding='3'>");

                strbCallDetails.Append("<tr><td style='background-color:#1B67A8; color:Red;'>");
                strbCallDetails.Append("<b>Approver is blocked</b>");
                strbCallDetails.Append("</td>");
            }
            
            return strbCallDetails.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Approver Grid Row Created Event

    protected void gvMappedApprover_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtFirstApprover = (TextBox)e.Row.FindControl("txtFirstApprover");
                //TextBox txtSecondApprover = (TextBox)e.Row.FindControl("txtSecondApprover");

                //UserControls_wucSearchmoreUserMapping ucFirst = (UserControls_wucSearchmoreUserMapping)e.Row.FindControl("WucSearchmoreUserMapping1");
                //UserControls_wucSearchmoreUserMapping ucSecond = (UserControls_wucSearchmoreUserMapping)e.Row.FindControl("WucSearchmoreUserMapping2");

                //ucFirst.TextboxName = txtFirstApprover.ClientID;
                //ucSecond.TextboxName = txtSecondApprover.ClientID;                

                PopupControlExtender pce1 = e.Row.FindControl("popCtrlExtAddPopUp1") as PopupControlExtender;
                PopupControlExtender pce2 = e.Row.FindControl("popCtrlExtAddPopUp2") as PopupControlExtender;

                string behaviorID1 = "pce1_" + e.Row.RowIndex;
                pce1.BehaviorID = behaviorID1;

                string behaviorID2 = "pce2_" + e.Row.RowIndex;
                pce2.BehaviorID = behaviorID2;

                Label lblFirstApprover = (Label)e.Row.FindControl("lblFirstApprover");
                Label lblSecondApprover = (Label)e.Row.FindControl("lblSecondApprover");

                string OnMouseOverScript1 = string.Format("$find('{0}').showPopup();", behaviorID1);
                string OnMouseOutScript1 = string.Format("$find('{0}').hidePopup();", behaviorID1);

                string OnMouseOverScript2 = string.Format("$find('{0}').showPopup();", behaviorID2);
                string OnMouseOutScript2 = string.Format("$find('{0}').hidePopup();", behaviorID2);

                lblFirstApprover.Attributes.Add("onmouseover", OnMouseOverScript1);
                lblFirstApprover.Attributes.Add("onmouseout", OnMouseOutScript1);

                lblSecondApprover.Attributes.Add("onmouseover", OnMouseOverScript2);
                lblSecondApprover.Attributes.Add("onmouseout", OnMouseOutScript2);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Approver Grid View Row Command Event

    protected void gvMappedApprover_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnReplace")
            {
                string strApproverRowID, strFApprover, strSApprover, strUserName;

                strApproverRowID = Convert.ToString(e.CommandArgument);
                strUserName = Membership.GetUser().UserName;

                GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = gr.RowIndex;

                TextBox txtFirstApprover = (TextBox)gvMappedApprover.Rows[index].FindControl("txtFirstApprover");
                TextBox txtSecondApprover = (TextBox)gvMappedApprover.Rows[index].FindControl("txtSecondApprover");

                strFApprover = txtFirstApprover.Text.Trim();
                strSApprover = txtSecondApprover.Text.Trim();

                UserRoleBAL objUserBAL = new UserRoleBAL();

                string strChkFirst = objUserBAL.CheckExistingMember(strFApprover);
                if (strChkFirst != "1")
                {
                    lblMessage.Text = "First approver does not exists Call Desk Database";
                    return;
                }

                if (strSApprover != "")
                {
                    string strChkSecond = objUserBAL.CheckExistingMember(strSApprover);
                    if (strChkSecond != "1")
                    {
                        lblMessage.Text = "Second approver does not exists Call Desk Database";
                        return;
                    }
                }

                ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();

                int intReturnVal = objBAL.ReplaceApprover(strApproverRowID, strFApprover, strSApprover, strUserName);

                if (intReturnVal == 1)
                {
                    lblMessage.Text = "Approver replace successfully.";
                    GetApproverMapping();
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

    #region Issue Request Sub Type Dropdown Selected Index Change

    protected void ddlIssueRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            lblIsBranchwise.Text = "";

            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();

            string strIssueRequestSubTypeID;
            strIssueRequestSubTypeID = ddlIssueRequestSubType.SelectedValue;

            string strReturnVal = objBAL.IsApproverBranchwise(strIssueRequestSubTypeID);

            hfReturnVal.Value = strReturnVal;
            
            if (strReturnVal == "0")
            {
                lblIsBranchwise.Text = "Mapping is direct.";
            }
            else if (strReturnVal == "1")
            {                
                lblIsBranchwise.Text = "Mapping is branchwise.";                
            }
            else
            {
                lblIsBranchwise.Text = "Mapping does not exists.";                
            }
            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Code Delete AIRST Approver Mapping

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            string intAIRSTID = ddlIssueRequestSubType.SelectedValue;
            string strUser = Membership.GetUser().UserName;
            objBAL.DeleteAIRSTApproverMapping(intAIRSTID, strUser);
            lblMessage.Text = "Approver Mapping Deleted Successfully.";
            ccdApplication.SelectedValue = "Select Application";
            lblIsBranchwise.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }        
    }
    
    #endregion
    
}
