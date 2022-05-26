using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBAL;
using CallDeskBO;
using AjaxControlToolkit;

public partial class Admin_AddAIRSubtypeMapping : System.Web.UI.Page
{
    private int intCnt;

    #region Page Level Varaibles

    ArrayList lasset = new ArrayList();
    ArrayList lsubordinate = new ArrayList();
    static ArrayList UpdateList = new ArrayList();

    #endregion

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        chkSelectAll.Attributes.Add("Onclick", "ChkUnChk('ctl00_ContentPlaceHolder1_chkSelectAll','ctl00_ContentPlaceHolder1_chkLocationType');");
        chkLocationType.Attributes.Add("Onclick", "CheckUnCheckAll('ctl00_ContentPlaceHolder1_chkSelectAll','ctl00_ContentPlaceHolder1_chkLocationType');");
        txtValidFrom.Attributes.Add("readonly", "true");
        txtValidTo.Attributes.Add("readonly", "true");
        ddlIssueRequest.Attributes.Add("OnChange", "HideTableRow();");

        if (!IsPostBack)
        {
            GetApplications();            
            // GetApproverAuthority();
            GetMappedApplicationIssueRequestSubType();
            // GetRoles();
            GetLocationType();
            GetGroups();
            CommonUtility.AddSelectToDropDown(ddlRole);

        }
    }

    #endregion

    #region Get Issue Request SubType

    private void GetIssueRequestSubType()
    {
        try
        {
            int intApplicationID, intIssueRequesTypeID;
            intApplicationID = Convert.ToInt32(ddlApplications.SelectedValue);
            intIssueRequesTypeID = Convert.ToInt32(ddlIssueRequestTypes.SelectedValue);
            AdminBAL objBAL = new AdminBAL();
            lstbxIssueRequestSubType.DataSource = objBAL.GetIssueRequestSubType(intApplicationID, intIssueRequesTypeID);
            lstbxIssueRequestSubType.DataTextField = "IssueRequestSubType";
            lstbxIssueRequestSubType.DataValueField = "IssueRequestSubType_PK";
            lstbxIssueRequestSubType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Get Application

    public void GetApplications()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            ddlApplications.DataSource = objBAL.GetApplicationType();
            ddlApplications.DataTextField = "ApplicationName";
            ddlApplications.DataValueField = "ApplicationID_PK";
            ddlApplications.DataBind();
            CommonUtility.AddSelectToDropDown(ddlApplications);
            CommonUtility.AddSelectToDropDown(ddlIssueRequestTypes);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Location Type

    public void GetLocationType()
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            chkLocationType.DataSource = objBAL.GetLocationType();
            chkLocationType.DataTextField = "Location_Type";
            chkLocationType.DataValueField = "TypeId";
            chkLocationType.DataBind();
            //CommonUtility.AddSelectToDropDown(ddlLocationType);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get ApproverAuthority

    public void GetApproverAuthority()
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            ddlApproverAuthority.DataSource = objBAL.GetApproverAuthority();
            ddlApproverAuthority.DataTextField = "ApproverAuthority";
            ddlApproverAuthority.DataValueField = "ApproverAuthorityID_PK";
            ddlApproverAuthority.DataBind();
            CommonUtility.AddSelectToDropDown(ddlApproverAuthority);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Get Roles

    public void GetRoles()
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            ddlRole.DataSource = objBAL.GetRole();
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleName";
            ddlRole.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Mapped Application Issue Request Sub Type

    public void GetMappedApplicationIssueRequestSubType()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            gvAIRSTMapping.DataSource = objBAL.GetMappedApplicationIssueRequestSubType();
            gvAIRSTMapping.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Issue Request Sub Type By Application Name

    public void GetMappedApplicationIssueRequestSubTypeByApplicationName()
    {
        try
        {
            RegisterNewCallBO objBO = new RegisterNewCallBO();
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            DropDownList ddlApplicationName = (DropDownList)gvAIRSTMapping.FooterRow.FindControl("ddlApplicationName");

            if (Convert.ToString(ddlApplicationName.SelectedValue) != "")
            {
                ViewState["ApplicationName"] = ddlApplicationName.SelectedItem.Text;
            }
            objBO.ApplicationName = Convert.ToString(ViewState["ApplicationName"]);
            gvAIRSTMapping.DataSource = objBAL.GetMappedApplicationIssueRequestSubTypeByApplicationName(objBO.ApplicationName);
            gvAIRSTMapping.DataBind();

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Selected Index Change

    protected void ddlApplications_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            ddlIssueRequestTypes.DataSource = objBAL.GetIssueRequestTypeByApplicationType(int.Parse(ddlApplications.SelectedValue));
            ddlIssueRequestTypes.DataTextField = "IssueRequestType";
            ddlIssueRequestTypes.DataValueField = "IssueRequestType_PK";
            ddlIssueRequestTypes.DataBind();
            CommonUtility.AddSelectToDropDown(ddlIssueRequestTypes);
            lstbxIssueRequestSubType.Items.Clear();
            lstbxSelectedIssueRequestSubType.Items.Clear();
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
            if (lstbxIssueRequestSubType.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstbxIssueRequestSubType.Items.Count; i++)
                {
                    if (lstbxIssueRequestSubType.Items[i].Selected)
                    {
                        if (!lasset.Contains(lstbxIssueRequestSubType.Items[i]))
                        {
                            lasset.Add(lstbxIssueRequestSubType.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lasset.Count; i++)
                {
                    if (!lstbxSelectedIssueRequestSubType.Items.Contains(((ListItem)lasset[i])))
                    {
                        lstbxSelectedIssueRequestSubType.Items.Add(((ListItem)lasset[i]));
                    }
                    lstbxIssueRequestSubType.Items.Remove(((ListItem)lasset[i]));
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
            if (lstbxSelectedIssueRequestSubType.SelectedItem != null)
            {
                for (int i = 0; i < lstbxSelectedIssueRequestSubType.Items.Count; i++)
                {
                    if (lstbxSelectedIssueRequestSubType.Items[i].Selected)
                    {
                        if (!lsubordinate.Contains(lstbxSelectedIssueRequestSubType.Items[i]))
                        {
                            lsubordinate.Add(lstbxSelectedIssueRequestSubType.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lsubordinate.Count; i++)
                {
                    if (!lstbxIssueRequestSubType.Items.Contains(((ListItem)lsubordinate[i])))
                    {
                        lstbxIssueRequestSubType.Items.Add(((ListItem)lsubordinate[i]));
                    }
                    lstbxSelectedIssueRequestSubType.Items.Remove(((ListItem)lsubordinate[i]));
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

    #region Create Application Issue Requets SubType Mapping Code

    protected void btnCreateMapping_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            List<AdminBOList> lstAdminBO = new List<AdminBOList>();
            AdminBO objAdminBO = new AdminBO();
            AdminBOList objAdminBOLst;

            objAdminBO.CreatedBy = Membership.GetUser().UserName;
            objAdminBO.ApplicationID = int.Parse(ddlApplications.SelectedValue);
            Session["ApplicationID"] = objAdminBO.ApplicationID;
            objAdminBO.IssueRequestID = int.Parse(ddlIssueRequestTypes.SelectedValue);
            Session["IssueRequestID"] = objAdminBO.IssueRequestID;
            objAdminBO.ValidFrom = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtValidFrom.Text));
            objAdminBO.ValidTo = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtValidTo.Text));
            //objAdminBO.Comment = txtComment.Text;
            objAdminBO.Priority = ddlPriority.SelectedValue;
            objAdminBO.Groups = ddlGroups.SelectedValue;
            Session["Groups"] = objAdminBO.Groups;
            objAdminBO.CallTAT = Convert.ToInt32(txtCallTAT.Text);
            Session["CallTAT"] = objAdminBO.CallTAT;
            objAdminBO.IssueRequestType = ddlIssueRequest.SelectedValue;
            if (ddlIssueRequest.SelectedValue != "Request")
            {
                objAdminBO.Email = 1;
                objAdminBO.SMS = 1;
                objAdminBO.ApproverAuthorityID = 0;
            }
            else
            {
                objAdminBO.Email = Convert.ToInt32(ddlMail.SelectedValue);
                objAdminBO.SMS = Convert.ToInt32(ddlSMS.SelectedValue);
                //objAdminBO.ApproverAuthorityID = Convert.ToInt32(ddlApproverAuthority.SelectedValue);
                //objAdminBO.Role = ddlRole.SelectedValue;
            }

            foreach (ListItem lstItem in lstbxSelectedIssueRequestSubType.Items)
            {
                objAdminBOLst = new AdminBOList();
                objAdminBOLst.IssueRequestSubTypeID = int.Parse(lstItem.Value);
                lstAdminBO.Add(objAdminBOLst);
            }

            objAdminBO.AdminBOList = lstAdminBO;

            List<LocationBOList> lstLocationBO = new List<LocationBOList>();
            LocationBOList lstBOLocation;
            if (chkSelectAll.Checked == true)
            {
                lstBOLocation = new LocationBOList();
                lstBOLocation.LocationTypeID = 0;
                lstLocationBO.Add(lstBOLocation);
            }
            else
            {
                for (int i = 0; i < chkLocationType.Items.Count; i++)
                {
                    if (chkLocationType.Items[i].Selected)
                    {
                        lstBOLocation = new LocationBOList();
                        lstBOLocation.LocationTypeID = Convert.ToInt32(chkLocationType.Items[i].Value);
                        lstLocationBO.Add(lstBOLocation);
                        //objAdminBAL.AddAIRSTOfficeTypeMapping(intReturnVal, chkLocationType.Items[i].Text);
                    }
                }
            }

            objAdminBO.LocationBOType = lstLocationBO;

            int intReturnVal = objAdminBAL.AddApplicationIssueRequestSubTypeMapping(objAdminBO);

            if (intReturnVal > 0)
            {
                //LitMessage.Text = "<div class='rcd-Welcome'>Application Issue Request SubType Mapping Created. For Adding SubType Purpose...<a href='AddAIRSTMComment.aspx'>go here</a></div>";
                lblMessage.Text = "Application issue request subtype mapping created, please go Administrator > Application Issue Request Sub Type Comment to add purpose";
                GetMappedApplicationIssueRequestSubType();
                ClearControls();
                txtValidFrom.Text = "";
                txtValidTo.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }   

    #endregion

    #region Clear Controls

    private void ClearControls()
    {
        ddlApplications.SelectedValue = "0";
        ddlIssueRequestTypes.SelectedValue = "0";
        lstbxIssueRequestSubType.Items.Clear();
        lstbxSelectedIssueRequestSubType.Items.Clear();
        ddlIssueRequest.SelectedValue = "0";
        txtValidFrom.Text = "";
        txtValidTo.Text = "";
        txtCallTAT.Text = "";
        ddlPriority.SelectedValue = "0";
        ddlGroups.SelectedValue = "0";
        chkSelectAll.Checked = false;
        chkLocationType.Items.Clear();
        GetLocationType();
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddAIRSubtypeMapping.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Page Index Changing Code for Application Issue Request SubType Mapping

    protected void gvAIRSTMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAIRSTMapping.PageIndex = e.NewPageIndex;
            if (Convert.ToString(ViewState["SortPaging"]) == "Yes")
            {
                GetMappedApplicationIssueRequestSubTypeByApplicationName();

            }
            else
            {
                GetMappedApplicationIssueRequestSubType();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Issue Request Type Mapping Row Command Event

    protected void gvAIRSTMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int intRowID, intReturnVal;


            if (e.CommandName == "lnkEdit")
            {
                intRowID = Convert.ToInt32(e.CommandArgument);
                Session["AIRSTRowId"] = intRowID;
                Response.Redirect("EditAIRSubtypeMapping.aspx");
            }
            else if (e.CommandName == "lnkDelete")
            {
                AdminBO objBO = new AdminBO();
                AdminBAL objBAL = new AdminBAL();
                intRowID = Convert.ToInt32(e.CommandArgument);
                objBO.DeletedBy = Membership.GetUser().UserName;
                //DropDownList ddlApplicationName = (DropDownList)gvAIRSTMapping.FooterRow.FindControl("ddlApplicationName");
                //ViewState["ApplicationName"] = ddlApplicationName.SelectedItem.Text;

                intReturnVal = objBAL.DeleteApplicationIssueRequestSubTypeMapping(intRowID, objBO.DeletedBy);

                if (ViewState["ApplicationName"] != null)// || (Convert.ToString(ViewState["ApplicationName"]) != ""))
                {
                    GetMappedApplicationIssueRequestSubTypeByApplicationName();
                    RefillListBoxIssueRequestType();
                    lblMessage.Text = "Mapping deleted successfully.";
                }
                else if (intReturnVal > 0)
                {
                    lblMessage.Text = "Mapping deleted successfully.";
                    RefillListBoxIssueRequestType();
                    GetMappedApplicationIssueRequestSubType();
                }
            }
            else if (e.CommandName == "btnSortApplications")
            {
                ViewState["SortPaging"] = "Yes";
                GetMappedApplicationIssueRequestSubTypeByApplicationName();
            }

            //if (e.CommandName == "lnkDetails")
            //{
            //    //string strRowId = Convert.ToString(e.CommandArgument);
            //    ////Session["TicketNumber"] = strTicketNumber;
            //    //// Response.Redirect("CallDetails.aspx");

            //    //StringBuilder stPopupScript = new StringBuilder();
            //    //stPopupScript.Append("<script language='javascript'>");
            //    //stPopupScript.Append("var w = window.open('CallDetails.aspx?','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
            //    //stPopupScript.Append("w.focus()");
            //    //stPopupScript.Append("</script>");

            //    //Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());
            //}
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Issue Request Mapping Grid Row Data Bound Event

    protected void gvAIRSTMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
                intCnt = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                intCnt++;
                ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
                ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkbtnDelete");
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

    #region Application Issue Request Sub Type Mapping DropDown Selected Index Changed Event

    protected void ddlIssueRequestTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIssueRequestSubType();
    }

    #endregion

    #region Service Method to Display data in Panel

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        try
        {
            DataSet dsDetails = new DataSet();
            DataTable dtDetails = new DataTable();

            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            dsDetails = objBAL.GetMappedApplicationIssueRequestSubType(contextKey);
            dtDetails = dsDetails.Tables[0];

            StringBuilder strbMappingDetails = new StringBuilder();

            strbMappingDetails.Append("<table style='background-color:#f3f3f3; border: #336699 2px solid; z-index:1000");
            strbMappingDetails.Append("width:350px; font-size:11px; font-family:Verdana, Arial, Tahoma, Helvetica, sans-serif;' cellspacing='0' cellpadding='3'>");

            strbMappingDetails.Append("<tr><td colspan='3' style='background-color:#1B67A8; color:#ffffff;'>");
            strbMappingDetails.Append("<b>Mapping Details</b>");
            strbMappingDetails.Append("</td></tr>");
            
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Valid From</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["ValidFrom"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Valid To</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["ValidTo"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Priority</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["Priority"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Groups</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["Groups"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Call TAT</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["CallTAT"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Send Email</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["SendEmail"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Send SMS</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["SendSMS"].ToString() + "</td></tr>");
            strbMappingDetails.Append("<tr><td align=" + "left" + "><b>Purpose</b></td><td><b>:</b></td><td align=" + "left" + ">" + dtDetails.Rows[0]["Description"].ToString() + "</td></tr>");

            strbMappingDetails.Append("</table>");

            return strbMappingDetails.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Row Created Event Code

    protected void gvAIRSTMapping_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopupControlExtender pce = e.Row.FindControl("popCtrlExtAddPopUp") as PopupControlExtender;

                string behaviorID = "pce_" + e.Row.RowIndex;
                pce.BehaviorID = behaviorID;

                Image img = (Image)e.Row.FindControl("imgLetter");

                string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                img.Attributes.Add("onmouseover", OnMouseOverScript);
                img.Attributes.Add("onmouseout", OnMouseOutScript);
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
            ddlGroups.DataSource = objBAL.GetGroups();
            ddlGroups.DataTextField = "Groups";
            ddlGroups.DataValueField = "Groups";
            ddlGroups.DataBind();
            CommonUtility.AddSelectToDropDown(ddlGroups);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Re fill ListBox IssueRequestSubType

    public void RefillListBoxIssueRequestType()
    {
        if (ddlApplications.SelectedIndex != 0 && ddlIssueRequestTypes.SelectedIndex != 0)
        {
            GetIssueRequestSubType();
        }
    }

    #endregion

}

