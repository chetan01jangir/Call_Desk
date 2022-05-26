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

public partial class Admin_GetApproverMappingByUser : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        WucSearchmoreUserMapping1.TextboxName = txtUserId.ClientID;
    }

    #endregion

    #region Code Button GetApprover Click

    protected void btnGetApprover_Click(object sender, EventArgs e)
    {
        try
        {
            string strUserCode;
            strUserCode = txtUserId.Text.Trim();
            GetApproverMapping(strUserCode);
            ViewState["vsUserId"] = strUserCode;
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
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
                    GetApproverMapping(ViewState["vsUserId"].ToString());
                }
            }

            if (e.CommandName == "btnReplaceAll")
            {
                string strApprover, strUserName, strNewApprover;
                strUserName = Membership.GetUser().UserName;
                UserRoleBAL objUserBAL = new UserRoleBAL();
                TextBox txtReplaceApprover = (TextBox)gvMappedApprover.FooterRow.FindControl("txtReplaceApprover");
                strApprover = txtUserId.Text.Trim();
                strNewApprover = txtReplaceApprover.Text.Trim();
                ViewState["vsApproverId"] = strNewApprover;
                string strChkFirst = objUserBAL.CheckExistingMember(strNewApprover);
                if (strChkFirst == "0")
                {
                    lblMessage.Text = "Approver " + "'" + strNewApprover + "'" + " does not exists in Call Desk Database";
                    return;
                }
                else
                {
                    ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
                    int intReturnValue = objBAL.ReplaceApproverForAllMapping(strApprover, strNewApprover, strUserName);

                    if (intReturnValue > 0)
                    {
                        lblMessage.Text = "Approver replace successfully.";
                        GetApproverMapping(ViewState["vsApproverId"].ToString());
                    }
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

    #region Get Approver Mapping Function

    private void GetApproverMapping(params object[] param)
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            gvMappedApprover.DataSource = objBAL.GetAIRSTMappedApproversByUserName(param);
            gvMappedApprover.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

}
