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
using System.Text;
using CallDeskBAL;
using CallDeskDAL;
using AjaxControlToolkit;
using System.IO;
using System.Xml;
using System.Xml.Xsl;


public partial class _Default : System.Web.UI.Page
{
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            BindCallDetailsForUsersOpenCall();
			BindApplications();
        }
    }

    #endregion

    #region Track Call Button Code

    protected void BtnTrackCall_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSearch.SelectedValue == "1")
            {
                BindCallDetailsGridByDate();
            }
            else if (ddlSearch.SelectedValue == "2")
            {
                BindCallDetailsGridByTicketNumber();
            }
            else if (ddlSearch.SelectedValue == "3")
            {
                BindCallDetailsGridByCallStatus();
            }
            else if (ddlSearch.SelectedValue == "4")
            {
                BindCallDetailsByPolicyNo();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Bind Call Details on Page Load For User(Open Call Only)

    public void BindCallDetailsForUsersOpenCall()
    {
        try
        {
            string strCallCreatedBy;
            DataSet dsCDData = new DataSet();
            TrackCallBAL objBAL = new TrackCallBAL();
           // strCallCreatedBy = Membership.GetUser().UserName;

            strCallCreatedBy = Convert.ToString(Session["AgentUserID"]);
            dsCDData = objBAL.GetCallDetailsForUsersOpenCall(strCallCreatedBy);

            ViewState["GridViewData"] = dsCDData;
            gvCallDetails.DataSource = dsCDData;
            gvCallDetails.DataBind();

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Bind Call Details Grid By Ticket Number

    public void BindCallDetailsGridByTicketNumber()
    {
        string strLoggedBranch, strCallCreatedBy;
        bool IsAdmin = Roles.IsUserInRole("Admin");
        DataSet dsCDData = new DataSet();
        TrackCallBAL objTCBAL = new TrackCallBAL();
        strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);

        //strCallCreatedBy = Membership.GetUser().UserName;
        strCallCreatedBy = Convert.ToString(Session["AgentUserID"]);

        //if (IsAdmin)
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByTicketNumberForAdmin(txtTicketNumber.Text.Trim());
        //}
        //else
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByTicketNumber(txtTicketNumber.Text.Trim(), strLoggedBranch, strCallCreatedBy);
        //}

        dsCDData = objTCBAL.GetCallDetailsByTicketNumber(txtTicketNumber.Text.Trim(), strLoggedBranch, strCallCreatedBy);

        ViewState["GridViewData"] = dsCDData;
        gvCallDetails.DataSource = dsCDData;
        gvCallDetails.DataBind();
    }

    #endregion

    #region Bind Call details By Call Status

    public void BindCallDetailsGridByCallStatus()
    {
        string strCallCreatedBy;
        //bool IsAdmin = Roles.IsUserInRole("Admin");
        DataSet dsCDData = new DataSet();
        TrackCallBAL objTCBAL = new TrackCallBAL();
        //strCallCreatedBy = Membership.GetUser().UserName;
        strCallCreatedBy =Convert.ToString(Session["AgentUserID"]);
        int ApplicationID = Convert.ToInt32(ddlApplication.SelectedValue);


        //if (IsAdmin)
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByCallStatusForAdmin(ddlCallStatus.SelectedValue);
        //}
        //else
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByCallStatus(ddlCallStatus.SelectedValue, strCallCreatedBy);
        //}
        dsCDData = objTCBAL.GetCallDetailsByCallStatus(ddlCallStatus.SelectedValue, strCallCreatedBy, ApplicationID);

        ViewState["GridViewData"] = dsCDData;
        gvCallDetails.DataSource = dsCDData;
        gvCallDetails.DataBind();
    }

    #endregion

    #region Bind Call Details Grid By Date

    public void BindCallDetailsGridByDate()
    {
        string strLoggedBranch, strCallCreatedBy;

        DataSet dsCDData = new DataSet();
        TrackCallBAL objTCBAL = new TrackCallBAL();
        //DateTime dtFromDate = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtFromDate.Text));
        //DateTime dtToDate = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtToDate.Text));
        DateTime dtFromDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime dtToDate = Convert.ToDateTime(txtToDate.Text);
        dtToDate = dtToDate.AddDays(1);
        strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);

        //strCallCreatedBy = Membership.GetUser().UserName;
        strCallCreatedBy = Convert.ToString(Session["AgentUserID"]);
        int ApplicationID = Convert.ToInt32(ddlApplication.SelectedValue);

        //bool IsAdmin = Roles.IsUserInRole("Admin");

        //if (IsAdmin)
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByDateForAdmin(dtFromDate, dtToDate);
        //}
        //else
        //{
        //    dsCDData = objTCBAL.GetCallDetailsByDate(dtFromDate, dtToDate, strLoggedBranch, strCallCreatedBy);
        //}

        dsCDData = objTCBAL.GetCallDetailsByDate(dtFromDate, dtToDate, strLoggedBranch, strCallCreatedBy, ApplicationID);
        ViewState["GridViewData"] = dsCDData;
        gvCallDetails.DataSource = dsCDData;
        gvCallDetails.DataBind();
    }

    #endregion

    #region Bind CallDetails By PolicyNo
    public void BindCallDetailsByPolicyNo()
    {
        try
        {
            //string strUserRole = System.Web.Security.Roles.GetRolesForUser()[0].ToString();
           // string strCallCreatedBy = Membership.GetUser().UserName;
            string strUserRole = "Agent";
            string strCallCreatedBy = Convert.ToString(Session["AgentUserID"]);

            string strPolicyNo = txtPolicyNo.Text.Trim();
            DataSet oDSPolicy = GetCallDetailsForPolicyNo(strUserRole, strCallCreatedBy, strPolicyNo);
            ViewState["GridViewData"] = oDSPolicy;
            gvCallDetails.DataSource = oDSPolicy;
            gvCallDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    #endregion    

    #region Call Details Grid Row Command Event

    protected void gvCallDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "lnkDetails")
            {
                string strTicketNumber = Convert.ToString(e.CommandArgument);
                Session["TicketNumber"] = strTicketNumber;
                // Response.Redirect("CallDetails.aspx");

                StringBuilder stPopupScript = new StringBuilder();
                stPopupScript.Append("<script language='javascript'>");
                stPopupScript.Append("var w = window.open('CallDetails.aspx?','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
                stPopupScript.Append("w.focus()");
                stPopupScript.Append("</script>");

                Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../IMD/Default.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Service Method to Display data in Panel

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        try
        {
            DataSet dsCallDetails = new DataSet();
            DataTable dtCallDetails = new DataTable();           

            TrackCallBAL objTCBAL = new TrackCallBAL();
           // string strUserName = Membership.GetUser().UserName;
            //string strUserName = Convert.ToString(Session["AgentUserID"]);
            //string strUserName = "70008042";

            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumberForAdmin(contextKey);
            dtCallDetails = dsCallDetails.Tables[0];

            string strCallType = Convert.ToString(dtCallDetails.Rows[0]["CallType"]);
            StringBuilder strbCallDetails = new StringBuilder();
           
            strbCallDetails.Append("<table style='background-color:#f3f3f3; border: #336699 2px solid; z-index:1000");
            strbCallDetails.Append("width:400px; font-size:11px; font-family:Verdana, Arial, Tahoma, Helvetica, sans-serif;' cellspacing='0' cellpadding='3'>");

            strbCallDetails.Append("<tr><td colspan='3' style='background-color:#1B67A8; color:#ffffff;'>");
            strbCallDetails.Append("<b>Call Details</b>");
            strbCallDetails.Append("</td></tr>");

            if (strCallType == "Request")
            {
                DataSet dsApprover = objTCBAL.GetReopenCallApprover(contextKey);
                if (dsApprover.Tables.Count > 0)
                {
                    DataTable dtApprover = new DataTable();
                    dtApprover = dsApprover.Tables[0];
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>First Approver Name</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtApprover.Rows[0]["ApproverName"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>First Approver Email</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtApprover.Rows[0]["ApproverMail"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>Second Approver Name</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtApprover.Rows[0]["SApproverName"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>Second Approver Email</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtApprover.Rows[0]["SApproverMail"]) + "</td></tr>");
                }
                else
                {
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>First Approver Name</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["ApproverName"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>First Approver Email</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["ApproverMail"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>Second Approver Name</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["SApproverName"]) + "</td></tr>");
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>Second Approver Email</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["SApproverMail"]) + "</td></tr>");
                }
                strbCallDetails.Append("<tr><td align=" + "left" + "><b>Approval Status</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["ApproverStatus"]) + "</td></tr>");
                if (Convert.ToString(dtCallDetails.Rows[0]["ApproverStatus"]) == "AutoClosed")
                {
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>AutoClosed Date</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["ApproverClosedDate"]) + "</td></tr>");
                }
                else
                {
                    strbCallDetails.Append("<tr><td align=" + "left" + "><b>Approved Date</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["ApproverClosedDate"]) + "</td></tr>");
                }
            }
            strbCallDetails.Append("<tr><td align=" + "left" + "><b>AppSupport Status</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["AppSupportStatus"]) + "</td></tr>");
            strbCallDetails.Append("<tr><td align=" + "left" + "><b>AppSupport Close Date</b></td><td><b>:</b></td><td align=" + "left" + ">" + Convert.ToString(dtCallDetails.Rows[0]["AppSupportCloseDate"]) + "</td></tr>");

            strbCallDetails.Append("</table>");

            return strbCallDetails.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Call Details Grid Row Created Event

    protected void gvCallDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (tc.HasControls())
                    {
                        LinkButton lnk = (LinkButton)tc.Controls[0];
                        if (lnk != null)
                        {
                            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                            img.ImageUrl = "~/Images/ico_" + (gvCallDetails.SortDirection == SortDirection.Ascending ? "asc" : "desc") + ".gif";
                            if (gvCallDetails.SortExpression == lnk.CommandArgument)
                            {
                                tc.Controls.Add(new LiteralControl(" "));
                                tc.Controls.Add(img);
                            }
                        }
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // PopupControlExtender pce1 = e.Row.FindControl("popCtrlExtAddPopUp") as PopupControlExtender;
                PopupControlExtender pce = e.Row.FindControl("popCtrlExtAddPopUp1") as PopupControlExtender;
                string behaviorID = "pce_" + e.Row.RowIndex;
                pce.BehaviorID = behaviorID;

                // Image img = (Image)e.Row.FindControl("imgLetter");
                Label lblTicketNo = (Label)e.Row.FindControl("lblTicketNo");


                string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                //img.Attributes.Add("onmouseover", OnMouseOverScript);
                //img.Attributes.Add("onmouseout", OnMouseOutScript);

                lblTicketNo.Attributes.Add("onmouseover", OnMouseOverScript);
                lblTicketNo.Attributes.Add("onmouseout", OnMouseOutScript);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Grid View Sorting

    protected void gvCallDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            string sortExpression = e.SortExpression;
			if (sortExpression == "CallDate")
				sortExpression = "SortDate";
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Sorting Function

    private void SortGridView(string sortExpression, string direction)
    {
        Session["sortGridAssign"] = sortExpression.ToString();
        Session["dirGridAssign"] = direction.ToString();

        DataSet dsCDData = new DataSet();
        dsCDData = (DataSet)ViewState["GridViewData"];

        DataTable dtCDData = new DataTable();
        dtCDData = dsCDData.Tables[0];

        DataView dvCDData = new DataView(dtCDData);

        dvCDData.Sort = sortExpression + direction;
		DataSet ds = new DataSet();
		ds.Merge(dvCDData.ToTable());
		ViewState["GridViewData"] = ds;
        gvCallDetails.DataSource = dvCDData;
        gvCallDetails.DataBind();
    }

    #endregion

    #region Set Sort Direction

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }

        set { ViewState["sortDirection"] = value; }
    }

    #endregion

    #region Call Details Grid View Page Index Changing Code

    protected void gvCallDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCallDetails.PageIndex = e.NewPageIndex;
//            if (ddlSearch.SelectedValue == "1")
//            {
//                BindCallDetailsGridByDate();
//            }
//            else if (ddlSearch.SelectedValue == "2")
//            {
//                BindCallDetailsGridByTicketNumber();
//            }
//            else if (ddlSearch.SelectedValue == "3")
//            {
//                BindCallDetailsGridByCallStatus();
//            }
//            else if (ddlSearch.SelectedValue == "4")
//            {
//                BindCallDetailsByPolicyNo();
//            }
			DataSet dsCDData = new DataSet();
			dsCDData = (DataSet)ViewState["GridViewData"];
			gvCallDetails.DataSource = dsCDData;
			gvCallDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Get CallDetails for PolicyNo
    public DataSet GetCallDetailsForPolicyNo(params object[] param)
    {
        DataSet dsDetailsByPolicyNo = new DataSet();
        DataTable dtDetailsByPolicyNo = new DataTable();
        try
        {
            dsDetailsByPolicyNo = DataUtils.ExecuteDataset("usp_GetCallDetailsByPolicyNo", param);


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return dsDetailsByPolicyNo;
    }
    #endregion

    #region Get ApplicationType By Branch
    public DataSet GetApplicationTypeByBranch(params object[] param)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {
            oDSApplicationTypes = DataUtils.ExecuteDataset("usp_GetApplicationTypeByBranch", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    #endregion

    #region Bind Applications
    public void BindApplications()
    {
        string strBranch = Convert.ToString(Session["LoggedBranch"]);
		string strAppType = Convert.ToString(Session["AppType"]);
        DataSet oDSApp = GetApplicationTypeByBranch(strBranch,strAppType);
        DataTable oDTApp = oDSApp.Tables[0];
        ddlApplication.Items.Clear();
        ddlApplication.DataSource = oDTApp;
        ddlApplication.DataTextField = "ApplicationName";
        ddlApplication.DataValueField = "ApplicationID_PK";
        ddlApplication.DataBind();
        ddlApplication.Items.Insert(0, new ListItem("All Applications", "0"));
    }
    #endregion

}

